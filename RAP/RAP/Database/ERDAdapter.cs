using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Database
{
    // Static?
    static class ERDAdapter
    {
        // MySQL connection details
        private const string server = "alacritas.cis.utas.edu.au";
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";

        // MySQL connection object
        private static MySqlConnection conn;

        static ERDAdapter()
        {
            string connectionString =
                String.Format("Data Source={0}; Database={1}" +
                " User Id={2}; Password={3}",
                server, db, user, pass);

            conn = new MySqlConnection(connectionString);
            conn.Open();
            Console.WriteLine("connected");
        }

        // Fetch all of researcher's publications from database.
        public static List<Model.Publication> fetchPublications(Model.Researcher r)
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT * FROM publication" +
                "WHERE doi=" +
                "(" +
                    "SELECT doi FROM researcher_publication" +
                    "WHERE id=?id" +
                ")", conn);

            cmd.Parameters.AddWithValue("id", r.Id);

            MySqlDataReader rdr = null;
            rdr = cmd.ExecuteReader();

            List<Model.Publication> publications = new List<Model.Publication>();

            while (rdr.Read())
            {
                // Add publication from database to list
                publications.Add(new Model.Publication
                {
                    Doi = rdr.GetString(rdr.GetOrdinal("doi")),
                    Title = rdr.GetString(rdr.GetOrdinal("title")),
                    //PublicationDate = rdr.GetDateTime(rdr.GetOrdinal("publication_date")),
                    PublicationDate = new DateTime(rdr.GetInt32((rdr.GetOrdinal("year"))), 0, 0),
                    Type = (PublicationType)Enum.Parse(
                        typeof(PublicationType), rdr.GetString(rdr.GetOrdinal("type"))),
                    CiteAs = rdr.GetString(rdr.GetOrdinal("cite_as")),
                    AvailabilityDate = rdr.GetDateTime(rdr.GetOrdinal("available"))
                });
            }

        return publications;
        }

        // Fetch list of researchers from database.
        //TODO:
        public static List<Model.Researcher> fetchResearcherList()
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT * FROM researcher", conn);

            MySqlDataReader rdr = null;
            rdr = cmd.ExecuteReader();

            List<Model.Researcher> researchers =
                new List<Model.Researcher>();

            while (rdr.Read())
            {
                Model.Staff staff;
                Model.Student student;
                Model.Researcher researcher = new Model.Researcher {
                    Id = rdr.GetInt32(rdr.GetOrdinal("id")),
                    FirstName = rdr.GetString(rdr.GetOrdinal("given_name")),
                    LastName = rdr.GetString(rdr.GetOrdinal("family_name")),
                    Title = rdr.GetString(rdr.GetOrdinal("title")),
                    Email = rdr.GetString(rdr.GetOrdinal("title")),
                    Photo = new Uri(rdr.GetString(rdr.GetOrdinal("photo"))),
                    StartInstitution = rdr.GetDateTime(rdr.GetOrdinal("utas_start")),
                    StartCurrentJob = rdr.GetDateTime(rdr.GetOrdinal("current_start"))
                };
                switch (rdr.GetString(rdr.GetOrdinal("type")))
                {
                    case "Staff":
                        staff = (Model.Staff)researcher;
                        staff.Level =
                            (EmploymentLevel)Enum.Parse(
                                typeof(EmploymentLevel),
                                rdr.GetString(rdr.GetOrdinal("level")));
                        break;
                    case "Student":
                        student = (Model.Student)researcher;
                        student.Degree = rdr.GetString(rdr.GetOrdinal("degree"));
                        student.SupervisorId = rdr.GetInt32(rdr.GetOrdinal("supervisor_id"));
                        break;
                    default:
                        break;
                }
            }

            return null;
        }

        // Fetch list of students staff member is or has ever supervised
        // from database.
        public static List<Model.Student> fetchSupervisions(Model.Staff s)
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT * FROM researcher" +
                "WHERE supervisor_id=?id", conn);

            cmd.Parameters.AddWithValue("id", s.Id);

            MySqlDataReader rdr = null;
            rdr = cmd.ExecuteReader();

            List<Model.Student> supervisions =
                new List<Model.Student>();
            while (rdr.Read())
            {
                supervisions.Add(new Model.Student {
                    Id = rdr.GetInt32(rdr.GetOrdinal("id")),
                    FirstName = rdr.GetString(rdr.GetOrdinal("given_name")),
                    LastName = rdr.GetString(rdr.GetOrdinal("family_name")),
                    Title = rdr.GetString(rdr.GetOrdinal("title")),
                    Email = rdr.GetString(rdr.GetOrdinal("title")),
                    Photo = new Uri(rdr.GetString(rdr.GetOrdinal("photo"))),
                    StartInstitution = rdr.GetDateTime(rdr.GetOrdinal("utas_start")),
                    StartCurrentJob = rdr.GetDateTime(rdr.GetOrdinal("current_start")),
                    Degree = rdr.GetString(rdr.GetOrdinal("degree")),
                    SupervisorId = rdr.GetInt32(rdr.GetOrdinal("supervisor_id"))
                });
            }

            return supervisions;
        }


        // Fetch publication from database.
        public static Model.Publication fetchPublicationDetails()
        {
            return null;
        }


        // Fetch list of researcher emails from database.
        public static List<string> fetchResearcherEmails(List<Model.Researcher> researchers)
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT email FROM researcher" +
                "WHERE id IN (@ids)", conn);

            /*
            var filter = from r in researchers
                         select r.Id;
            List<int> researcherIds = new List<int>(filter);
            */
            //List<MySqlParameter> parameters = new List<MySqlParameter>();
            List<int> researcherIds = new List<int> { 
                123460, 123461, 123462, 123463
            };

            foreach (int i in researcherIds)
                cmd.Parameters.Add(new MySqlParameter { Value=i });

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<string> emails = new List<string>();

            while (rdr.Read())
            { 
                emails.Add(rdr.GetString(rdr.GetOrdinal("email")));
            }
            foreach (string email in emails)
                Console.WriteLine(email);

            return emails;
        }

        // Fetch list of researcher's publications from database.
        public static List<string> fetchPublicationList(Model.Researcher r)
        {

            return null;
        }

        // Fetch researcher from database.
        public static Model.Researcher fetchResearcher(int id)
        {
            return null;
        }

        // Fetch list of staff from database.
        public static List<Model.Staff> fetchStaffList()
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT * FROM researcher" +
                "WHERE type=staff", conn);

            MySqlDataReader rdr = null;
            rdr = cmd.ExecuteReader();

            List<Model.Staff> staff =
                new List<Model.Staff>();

            while (rdr.Read())
            {
                staff.Add( new Model.Staff {
                    Id = rdr.GetInt32(rdr.GetOrdinal("id")),
                    FirstName = rdr.GetString(rdr.GetOrdinal("given_name")),
                    LastName = rdr.GetString(rdr.GetOrdinal("family_name")),
                    Title = rdr.GetString(rdr.GetOrdinal("title")),
                    Email = rdr.GetString(rdr.GetOrdinal("title")),
                    Photo = new Uri(rdr.GetString(rdr.GetOrdinal("photo"))),
                    StartInstitution = rdr.GetDateTime(rdr.GetOrdinal("utas_start")),
                    StartCurrentJob = rdr.GetDateTime(rdr.GetOrdinal("current_start")),
                    Level = (EmploymentLevel)Enum.Parse(
                        typeof(EmploymentLevel),
                        rdr.GetString(rdr.GetOrdinal("level")))
                });
            }
            return staff;
        }

        //???
        public static string fetchResearcherName()
        {
            return null;
        }
    }
}
