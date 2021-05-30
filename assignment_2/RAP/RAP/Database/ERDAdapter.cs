using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace RAP.Database
{
    abstract class ERDAdapter
    {
        // MySQL connection details
        private const string server = "alacritas.cis.utas.edu.au";
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";

        // MySQL connection object
        private static MySqlConnection conn;
        protected static MySqlConnection Conn
        {
            get { return conn; }
        }


        private static MySqlDataReader rdr;
        protected static MySqlDataReader Rdr { get; set; }


        // Error reporting flag
        private static bool reportingErrors = true;


        public static readonly Dictionary<Type, ResearcherType> type =
        new Dictionary<Type, ResearcherType> {
            { typeof(Model.Staff), ResearcherType.STAFF },
            { typeof(Model.Student), ResearcherType.STUDENT }
        };

        static ERDAdapter()
        {
            string connectionString =
                String.Format("Data Source={0}; Database={1}; " +
                "User Id={2}; Password={3}",
                server, db, user, pass);


            conn = new MySqlConnection(connectionString);
            // Test connection to MySQL database once at startup
            try
            {
                conn.Open();
            }
            catch (MySqlException e)
            {
                Error("connecting to the database", e);
                if (conn != null) conn.Close();
                // Exit program if database not found
                System.Environment.Exit(1);
            }
            finally
            {
                // Database succesfully connected, end the test
                if (conn != null) conn.Close();
            }

            Rdr = null;
        }

        protected static string GetString(string columnName)
        {
            int columnIndex = Rdr.GetOrdinal(columnName);

            return Rdr.IsDBNull(columnIndex)
                ? null
                : Rdr.GetString(columnIndex);
        }

        protected static int? GetInt(string columnName)
        {
            int columnIndex = Rdr.GetOrdinal(columnName);

            return Rdr.IsDBNull(columnIndex)
                ? null
                : (int?)Rdr.GetInt32(columnIndex);
        }


        protected static DateTime? GetDateTime(string columnName)
        {
            int columnIndex = Rdr.GetOrdinal(columnName);

            return Rdr.IsDBNull(columnIndex)
                ? null
                : (DateTime?)Rdr.GetDateTime(columnIndex);
        }

        protected static Uri GetUri(string columnName)
        {
            int columnIndex = Rdr.GetOrdinal(columnName);

            return Rdr.IsDBNull(columnIndex)
                ? null
                : new Uri(Rdr.GetString(columnName));
        }

        protected static int? GetInt64(string columnName)
        {
            int columnIndex = Rdr.GetOrdinal(columnName);

            return Rdr.IsDBNull(columnIndex)
                ? null
                : (int?)Rdr.GetInt64(columnIndex);
        }
        // Year is stored in the database as a 16-bit integer.
        // Convert it to DateTime
        protected static DateTime? GetYear(string columnName)
        {
            int columnIndex = Rdr.GetOrdinal(columnName);

            return Rdr.IsDBNull(columnIndex)
                ? null
                : (DateTime?)(new DateTime(
                    Rdr.GetInt16(columnIndex), 1, 1));
        }

        // Enums cannot be made nullable.
        // TODO: null safe?
        //       maybe, so long as all enums have NULL value
        protected static T GetEnum<T>(string columnName)
            where T : System.Enum
        {
            return (T)Enum.Parse(typeof(T), GetString(columnName).ToUpper().Replace(" ", "_"));
        }

        protected static void Error(string msg, Exception e)
        { 
            if (reportingErrors)
            {
                MessageBox.Show($"An error occurred while {msg}.  Try again later.");
                MessageBox.Show($"\nError Details:\n {e}\n");
            }
        }
    }
}
