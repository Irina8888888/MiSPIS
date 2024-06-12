using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;






namespace MiSPIS
{
    public partial class Form : System.Windows.Forms.Form
    {

        private SqlConnection connection = null;
       

        public Form()
        {
            InitializeComponent();
            
            //hashingService = new HashingService();
            textBox3.UseSystemPasswordChar = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            connection.Open();
        }

        private void АВТОРИЗАЦИЯ_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form2 fr = new Form2();
            //fr.Txt = this.textBox3.Text;

            //fr.ShowDialog();
            DataBase db = new DataBase();


            string loginUser = textBox4.Text;
            string passwordUser = textBox3.Text;
            string hashedPassword = Hach.PWhash(passwordUser); // Хешируем пароль

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            db.OpenConnection();
            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE login = @uL AND pass= @uP", db.GetConnection());
            command.Parameters.Add("@uL", SqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", SqlDbType.VarChar).Value = hashedPassword;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                // Пользователь успешно авторизован
                MessageBox.Show("Вы успешно вошли в систему");
                this.Hide();
                Form2 mainForm = new Form2(); // Основная форма после входа
                mainForm.Show();
            }
            else
            {
                // Пользователь не найден
                MessageBox.Show("Пользователь не найден");
            }
            db.CloseConnection();
        }

       
               
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       private void checkBox1_CheckedChanged(object sender, EventArgs e)
       {
            if (checkBox1.Checked)
            {
                textBox3.UseSystemPasswordChar = false;

            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
       }

        private void button3_Click(object sender, EventArgs e)
        {
            RegisForm f = new RegisForm();
            f.Show();
            this.Hide();
        }
    }
}