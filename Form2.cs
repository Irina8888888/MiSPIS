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

        private SqlConnection sqlConnection = null;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SqlDataAdapter dataAdapter = new SqlDataAdapter(textBox1.Text, SqlConnection);

            DataSet ds = new DataSet();
           // dataAdapter.Fill(ds);

           // DataGridView.DataSource = DataSet.Tables[0];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            
            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
             SqlCommand command = new SqlCommand
                ($"INSERT INTO [Clients] (full_name,work_address,district,district_name) VALUES (@full_name,@work_address,@district,@district_name')", sqlConnection);

            command.Parameters.AddWithValue("full_name", textBox2.Text);
            command.Parameters.AddWithValue("work_address", textBox3.Text);
            command.Parameters.AddWithValue("district", textBox4.Text);
            command.Parameters.AddWithValue("district_name", textBox5.Text);

            //command.ExecuteNonQuery();
            MessageBox.Show(command.ExecuteNonQuery().ToString());

             
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
