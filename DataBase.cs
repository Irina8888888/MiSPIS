using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MiSPIS
{
    class DataBase
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programming\\C_sharp\\Authorization_MSSQL_VisualStudio\\my.ini;Integrated Security=True");


        public void openConnection()
        { 
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public  SqlConnection GetConnection()
        {
            return connection;
        }

    }
}

//server=localhost;port=3306;username=root;password=123456789;database=sys;Integrated Security=True"

//Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Programming\\C_sharp\\Authorization_MSSQL_VisualStudio\\alexsav.mdf;Integrated Security=True