using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Configuration;


namespace MiSPIS
{
    public partial class Login : Form
    {
//подключение БД

        DataBase dataBase = new DataBase();
        public Login()
        {
            InitializeComponent();
            textBox3.UseSystemPasswordChar = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void АВТОРИЗАЦИЯ_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "qwe" && textBox3.Text == "1234")
            { 
                Form2 s = new Form2();
                s.Show();

                this.Hide();
            }
            else
            {
                textBox4.Text = "";
                textBox3.Text = "";
                MessageBox.Show("Неправильный логин или пароль");
            }

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