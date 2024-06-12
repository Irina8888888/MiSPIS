using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MiSPIS;


namespace MiSPIS
{
    public partial class RegisForm : System.Windows.Forms.Form
    {
        int lastID;
        public int lastIDforTests;
        public string loginForTests, hashForTests;

        private SqlConnection connection = null;
        public RegisForm()
        {
            
            InitializeComponent();
            textBox1.ForeColor = Color.Gray; textBox1.Text = "Введите фамилию"; 
            textBox2.ForeColor = Color.Gray; textBox2.Text = "Введите имя";
            textBox3.ForeColor = Color.Gray; textBox3.Text = "Введите логин";
            textBox4.ForeColor = Color.Gray; textBox4.Text = "Введите пароль"; textBox4.UseSystemPasswordChar = false;
            textBox5.ForeColor = Color.Gray; textBox5.Text = "Повторите пароль"; textBox5.UseSystemPasswordChar = false;
            label1.Select();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введите фамилию") { textBox1.Text = ""; textBox1.ForeColor = Color.Black; }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "Введите имя") { textBox2.Text = ""; textBox2.ForeColor = Color.Black; }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "Введите логин") { textBox3.Text = ""; textBox3.ForeColor = Color.Black; }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "Введите пароль") { textBox4.Text = ""; textBox4.ForeColor = Color.Black; }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "Повторите пароль") { textBox5.Text = ""; textBox5.ForeColor = Color.Black; }
        }

        public Boolean IsUsersExist()
        {
            DataBase db = new DataBase();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE login = @uL", db.GetConnection());
            command.Parameters.Add("@uL", SqlDbType.NVarChar).Value = textBox3.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует");
                textBox3.Select();
                //textBox3.SelectionStart = 0;
                //textBox3.SelectionLength = textBox3.Text.Length;
                return true;
            }
            else return false;
        }

        public int CheckLastUsersID()
        {
            DataBase db = new DataBase();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT id FROM Users " +
                                               "ORDER BY id DESC", db.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);
            lastID = Convert.ToInt32(table.Rows[0]["id"].ToString());
            return lastID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox3.Text.TrimEnd(new Char[] { ' ' });        // удаление пробелов, если стоит после логина
            if (textBox2.Text == "Введите имя" || textBox1.Text == "Введите фамилию" || textBox3.Text == "Введите логин" || textBox4.Text == "Введите пароль" || textBox5.Text == "Повторите пароль")
            { MessageBox.Show("Заполните все поля"); return; }

            if (textBox4.Text != textBox5.Text)
            { MessageBox.Show("Пароли не совпадают"); return; }

            if (IsUsersExist()) return;
            RegisNewUser(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);

            this.Close();
            RegisForm form = new RegisForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Show();
            this.Close();
        }

        public void RegisNewUser(string surname, string name, string login, string password)
        {
            CheckLastUsersID(); lastIDforTests = lastID + 1;
            DataBase db = new DataBase();
            SqlCommand command = new SqlCommand("INSERT INTO Users (id, surname, name, login, pass) VALUES (@id, @surname, @name, @login, @pass)", db.GetConnection());
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = lastID + 1;
            command.Parameters.Add("@surname", SqlDbType.NVarChar).Value = surname;
            command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
            command.Parameters.Add("@login", SqlDbType.NVarChar).Value = login;
            command.Parameters.Add("@pass", SqlDbType.NVarChar).Value = Hach.PWhash(password);

            db.OpenConnection();

            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Ошибка создания учетной записи");
            }
            else
            {
                MessageBox.Show("Учетная запись создана успешно");
            }



            db.CloseConnection();
        }

        public void CompareForTests()
        {
            Comparison();
        }

        private void Comparison()
        {
            DataBase db = new DataBase();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT id, login FROM Users " +
                                               "ORDER BY id DESC", db.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            lastIDforTests = (Int32)table.Rows[0]["id"];
            loginForTests = table.Rows[0]["login"].ToString();
        }


    }
}
