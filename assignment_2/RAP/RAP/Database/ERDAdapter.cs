using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Database
{
    static class ERDAdapter
    {
        // MySQL connection details
        private const string server = "alacritas.cis.utas.edu.au";
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";

        // MySQL connection object
        private static MySqlConnection conn;

        // Error reporting flag
        private static bool reportingErrors = true;

        public static MySqlConnection Conn
        {
            get { return conn; }
        }

        public static readonly Dictionary<Type, ResearcherType> type =
        new Dictionary<Type, ResearcherType> {
            { typeof(Model.Staff), ResearcherType.Staff },
            { typeof(Model.Student), ResearcherType.Student }
        };

        static ERDAdapter()
        {
            string connectionString =
                String.Format("Data Source={0}; Database={1}; " +
                "User Id={2}; Password={3}",
                server, db, user, pass);

            conn = new MySqlConnection(connectionString);
            // conn.Open();
        }

        /*
        public static MySqlConnection getConnection()
        {
            return null;
        }
        */

        public static void Error(string msg, Exception e)
        { 
            if (reportingErrors)
            {
                Console.WriteLine($"An error occurred while {msg}.  Try again later.");
                Console.WriteLine($"\nError Details:\n {e}\n");
            }
        }
    }
}
