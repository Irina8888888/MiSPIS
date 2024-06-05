using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace MiSPIS
{
    class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(Localhost); Server=Localhost;Port=5412;Database=postgres;Integrated Security=True");
   

        public void OpenConnection()
        {
           if (sqlConnection.State == System.Data.ConnectionState.Closed)
           {
            sqlConnection.Open();
           }
        }

        public void CloseConnection()
        {

           if (sqlConnection.State == System.Data.ConnectionState.Open)
           {
            sqlConnection.Close();
           }
        }

       public SqlConnection GetConnection()
       {
        return sqlConnection;
       }

    }
}

