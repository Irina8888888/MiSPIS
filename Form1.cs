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



namespace MiSPIS
{
    public partial class Form1 : Form
    {

        string sql = "Server = Localhost; Port=5412;Database=Kurs;";
       

        //подключение базы данных
        public Form1()
        {
            InitializeComponent();

            sqlConectionReader();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sqlConectionReader()
        {
            NpgsqlConnection sqlConnession = new NpgsqlConnection();// считывает данные sql
            sqlConnession.Open();// открываем соединение с БД
            NpgsqlCommand command = new NpgsqlCommand(sql);//инициализация класса
            command.Connection = sqlConnession;//передаем строку подключения к БД
            command.CommandType = CommandType.Text;// тип команды
            command.CommandText = "SELECT*FROM Clients"; //команда
            NpgsqlDataReader dataReader = command.ExecuteReader();//для извлечения данных из БД
            if (dataReader.HasRows) //проверяем содержит ли одну или несколько строк
            {
                DataTable data = new DataTable();
                data.Load(dataReader);
                dataGridView1 = data;//таблица
            }
            command.Dispose();
            sqlConnession.Close();//закрываем соединени с БД
        }
    }
}
