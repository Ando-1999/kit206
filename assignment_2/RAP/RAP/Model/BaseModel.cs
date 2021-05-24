using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RAP.Database;

namespace RAP.Model
{

    class BaseModel
    {
        // MySQL connection details
        private const string server = "alacritas.cis.utas.edu.au";
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";

        protected static MySqlConnection connection = null;

        private static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                string connectionString =
                String.Format("Database={0};Data Source={1};User Id={2}; Password={3}", db, server, user, pass);
                connection = new MySqlConnection(connectionString);
            }
            return connection;
        }

        public static MySqlDataReader DataReaderAll(string TableName)
        {
            if (TableName == null)
            {
                return null;
            }
            string sql = $"SELECT * FROM {TableName}";
            GetConnection().Open();
            MySqlCommand command = new MySqlCommand(sql, connection);
            //command.Parameters.AddWithValue("table", TableName);
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            return dataReader;
        }


        protected static string GetString(MySqlDataReader dr, string columnName)
        {
            return dr.IsDBNull(dr.GetOrdinal(columnName)) ? null : dr.GetString(dr.GetOrdinal(columnName));
        }

        protected static int GetInt32(MySqlDataReader dr, string columnName)
        {
            return dr.IsDBNull(dr.GetOrdinal(columnName)) ? -1 : dr.GetInt32(dr.GetOrdinal(columnName));
        }
    }
}
