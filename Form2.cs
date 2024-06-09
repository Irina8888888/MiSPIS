using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MiSPIS
{
    public partial class Form2 : Form
    {

        public SqlConnection sqlConnection = null;

        public string Txt
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

    
        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }
            // заполняем   dataGridView
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT*FROM CLients, Housing, HousingExchange", sqlConnection);

            DataSet db = new DataSet();
            dataAdapter.Fill(db);
            dataGridView1.DataSource = db.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //поиск
        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(textBox1.Text,sqlConnection);

            DataSet dataSet = new DataSet(); //обьектное представление БД
            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"full_name  LIKE '%{textBox1.Text}%'";
        }


        //добавление
             private void button3_Click(object sender, EventArgs e)
        {
             SqlCommand command = new SqlCommand
                ($"INSERT INTO [Clients] (full_name,work_address,district,district_name) VALUES (@full_name,@work_address,@district,@district_name)", sqlConnection);

            command.Parameters.AddWithValue("full_name", textBox2.Text);
            command.Parameters.AddWithValue("work_address", textBox3.Text);
            command.Parameters.AddWithValue("district", textBox4.Text);
            command.Parameters.AddWithValue("district_name", textBox5.Text);

           
            MessageBox.Show(command.ExecuteNonQuery().ToString());
           
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand
               ($"INSERT INTO [Housing] (full_name,address,num_rooms,district_name) VALUES (@full_name,@address,@num_rooms,@district_name)", sqlConnection);

            command.Parameters.AddWithValue("full_name", textBox6.Text);
            command.Parameters.AddWithValue("address", textBox7.Text);
            command.Parameters.AddWithValue("num_rooms", textBox8.Text);
            command.Parameters.AddWithValue("district_name", textBox9.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand
               ($"INSERT INTO [HousingExchange] (full_name1,address1,num_rooms1,district_name1,full_name2,address2,num_rooms2,district_name2,data) VALUES (@full_name1,@address1,@num_rooms1,@district_name1,@full_name2,@address2,@num_rooms2,@district_name2,@exchange_data)", sqlConnection);

            DateTime data = DateTime.Parse(textBox18.Text);//!!!!

            command.Parameters.AddWithValue("full_name1", textBox10.Text);
            command.Parameters.AddWithValue("address1", textBox11.Text);
            command.Parameters.AddWithValue("num_rooms1", textBox12.Text);
            command.Parameters.AddWithValue("district_name1", textBox13.Text);
            command.Parameters.AddWithValue("full_name2", textBox14.Text);
            command.Parameters.AddWithValue("address2", textBox15.Text);
            command.Parameters.AddWithValue("num_rooms2", textBox16.Text);
            command.Parameters.AddWithValue("district_name2", textBox17.Text);
            command.Parameters.AddWithValue("exchange_data", $"{data.Month}/{data.Day}/{data.Year}");

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            string Message;
            Message = "Вы действительно хотите удалить запись?";

            if (MessageBox.Show(Message, "Удаление",MessageBoxButtons.YesNo,MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return;
            }

            string id;
            id = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();

            string sql = "Delete from DB where id =" + id;
            //ExecSql(sql);

        }
    }

}
