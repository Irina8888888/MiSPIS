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






namespace MiSPIS
{
    public partial class Login : Form
    {

        private SqlConnection connection = null;
       

        public Login()
        {
            InitializeComponent();
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
            Form2 fr = new Form2();
            fr.Txt = this.textBox3.Text;

            fr.ShowDialog();

           String loginUsers = textBox4.Text;
           String passUsers = textBox3.Text;

            DataBase db = new DataBase();

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();


            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE login = @uL AND pass= @uP", db.GetConnection());
            command.Parameters.Add("@uL",SqlDbType.VarChar).Value = loginUsers;
            command.Parameters.Add("@uP",SqlDbType.VarChar).Value = passUsers;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                MessageBox.Show("Успешно");
            else
                MessageBox.Show("Неудача");

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
              
        }
    }
}