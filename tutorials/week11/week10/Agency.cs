using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace week10
{
    class Agency
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";
        private static MySqlConnection conn;

        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString =
                    String.Format("Database={0};Data Source={1};User Id={2};Password={3}",
                    db, server, user, pass);

                conn = new MySqlConnection(connectionString);
            }

            return conn;
        }

        public static List<Employee> LoadAll()
        {
            if (conn == null)
                GetConnection();

            List<Employee> employeeList = null;
            MySqlDataReader rdr = null;

            try {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT id, given_name, family_name FROM researcher", conn);

                rdr = cmd.ExecuteReader();

                employeeList = new List<Employee>();

                while (rdr.Read())
                {
                    employeeList.Add(new Employee {
                        Id = (int)rdr["id"],
                        Name = rdr["family_name"] + ", " + rdr["given_name"],
                    });
                }
            }
            finally {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }

            return employeeList;
        }

        public static List<TrainingSession> LoadTrainingSessions(int id)
        {
            if (conn == null)
                GetConnection();

            MySqlDataReader rdr = null;
            List<TrainingSession> trainingList = null;

            try {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT title, makedate(year, 1) as yr, type, available " +
                    "FROM publication AS pub, researcher_publication AS respub " +
                    "WHERE pub.doi=respub.doi AND researcher_id=?id", conn);

                cmd.Parameters.AddWithValue("id", id);

                rdr = cmd.ExecuteReader();

                trainingList = new List<TrainingSession>();

                while (rdr.Read())
                {
                    Mode Mode = (Mode)Enum.Parse(typeof(Mode), (string)rdr["type"]);
                    trainingList.Add( new TrainingSession {
                        Title = (string)rdr["title"],
                        Year = (DateTime)rdr["yr"],
                        Certified = (DateTime)rdr["available"],
                        Mode = (Mode)Enum.Parse(typeof(Mode), (string)rdr["type"])
                    });
                }
            }
            finally {
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }

            return trainingList;
        }
    }
}
