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

        private static readonly Dictionary<Type, ResearcherType> type =
        new Dictionary<Type, ResearcherType> {
            { typeof(Model.Researcher), ResearcherType.Researcher },
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
            conn.Open();
        }

        // Fetch all of a researcher's publications from database.
        // DONE
        public static List<Model.Publication> fetchPublicationsList(Model.Researcher r)
        {
            MySqlCommand cmd = new MySqlCommand(
                "SELECT doi, title, year FROM publication " +
                "WHERE doi IN" +
                "(" +
                    "SELECT doi FROM researcher_publication " +
                    "WHERE researcher_id=?id" +
                ")", conn);

            cmd.Parameters.AddWithValue("id", r.Id);

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Model.Publication> publications =
                new List<Model.Publication>();

            while (rdr.Read())
            {
                // Add publication from database to list
                publications.Add(new Model.Publication
                {
                    Doi = (string)rdr["doi"],
                    Title = (string)rdr["title"],
                    PublicationYear = new DateTime((Int16)rdr["year"], 1, 1)
                });
            }

            rdr.Close();
            return publications;
        }

        // Fetch list of researchers from database.
        // DONE
        public static List<Model.Researcher> fetchResearcherList()
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT id, family_name, given_name," +
                "title, type FROM researcher", conn);

            MySqlDataReader rdr = null;
            rdr = cmd.ExecuteReader();

            List<Model.Researcher> researchers =
                new List<Model.Researcher>();

            while (rdr.Read())
            {
                Model.Researcher researcher = null;
                switch (Enum.Parse(typeof(ResearcherType),
                    (string)rdr["type"]))
                {
                    case ResearcherType.Staff:
                        researcher = new Model.Staff();
                        break;
                    case ResearcherType.Student:
                        researcher = new Model.Student();
                        break;
                    default:
                        break;
                }
                researcher.Id = (int)rdr["id"];
                researcher.FirstName = (string)rdr["given_name"];
                researcher.LastName = (string)rdr["family_name"];
                researcher.Title = (string)rdr["title"];
                researchers.Add(researcher);
            }

            rdr.Close();
            return researchers;
        }
        // Assumes Researcher object already contains Id, FirstName,
        // LastName, and title
        // DONE
        public static Model.Researcher fetchResearcherDetails(Model.Researcher r)
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT * FROM researcher WHERE id=?id", conn);

            cmd.Parameters.AddWithValue("id", r.Id);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                switch ((string)rdr["type"])
                {
                    case "Staff":
                        ((Model.Staff)r).Level =
                            (EmploymentLevel)Enum.Parse(
                                typeof(EmploymentLevel),
                                (string)rdr["level"]);
                        break;
                    case "Student":
                        ((Model.Student)r).Degree =
                            (string)rdr["degree"];
                        ((Model.Student)r).SupervisorId =
                            (int)rdr["supervisor_id"];
                        break;
                    default:
                        break;
                }

                r.Email = (string)rdr["email"];
                r.Photo = new Uri((string)rdr["photo"]);
                r.StartInstitution = (DateTime)rdr["utas_start"];
                r.StartCurrentJob = (DateTime)rdr["current_start"];
            }

            rdr.Close();
            return r;
        }

        // Fetch list of students staff member is or has ever supervised
        // from database.
        // DONE
        public static List<Model.Student> fetchSupervisions(Model.Staff s)
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT * FROM researcher " +
                "WHERE supervisor_id=?id", conn);

            cmd.Parameters.AddWithValue("id", s.Id);

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Model.Student> supervisions =
                new List<Model.Student>();

            while (rdr.Read())
            {
                supervisions.Add(new Model.Student {
                    Id = (int)rdr["id"],
                    FirstName = (string)rdr["given_name"],
                    LastName = (string)rdr["family_name"],
                    Title = (string)rdr["title"],
                    Degree = (string)rdr["degree"]
                });
            }

            rdr.Close();
            return supervisions;
        }


        // Fetch publication from database.
        // DONE
        public static Model.Publication fetchPublicationDetails(Model.Publication publication)
        {
            MySqlCommand cmd = new MySqlCommand(
                "SELECT authors, type, cite_as, available " +
                "FROM publication WHERE doi=?doi", conn);

            cmd.Parameters.AddWithValue("doi", publication.Doi);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                publication.Authors = (string)rdr["authors"];
                publication.Type = (PublicationType)Enum.Parse(
                    typeof(PublicationType), (string)rdr["type"]);
                publication.CiteAs = (string)rdr["cite_as"];
                publication.AvailabilityDate =
                    (DateTime)rdr["available"];
            }

            rdr.Close();
            return publication;
        }

        // Fetch list of researcher emails from database.
        // DONE
        public static List<string> fetchResearcherEmails(List<Model.Researcher> researchers)
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT email FROM researcher " +
                "WHERE FIND_IN_SET(id, ?ids) != 0", conn);

            var filter = from r in researchers
                         select r.Id;
            List<int> researcherIds = new List<int>(filter);

            cmd.Parameters.AddWithValue("ids", String.Join(",", researcherIds));

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<string> emails = new List<string>();

            while (rdr.Read())
                emails.Add((string)rdr["email"]);

            rdr.Close();
            return emails;
        }

        // Fetch list of staff from database.
        // TODO: Unimportant?
        // DONE
        public static List<Model.Staff> fetchStaffList()
        {
            MySqlCommand cmd =
                new MySqlCommand("SELECT * FROM researcher " +
                "WHERE type='Staff'", conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Model.Staff> staff = new List<Model.Staff>();

            while (rdr.Read())
            {
                staff.Add( new Model.Staff {
                    Id = (int)rdr["id"],
                    FirstName = (string)rdr["given_name"],
                    LastName = (string)rdr["family_name"],
                    Title = (string)rdr["title"],
                    Email = (string)rdr["title"],
                    Photo = new Uri((string)rdr["photo"]),
                    StartInstitution = (DateTime)rdr["utas_start"],
                    StartCurrentJob = (DateTime)rdr["current_start"],
                    Level = (EmploymentLevel)Enum.Parse(
                        typeof(EmploymentLevel), (string)rdr["level"])
                });
            }
            rdr.Close();
            return staff;
        }
    }
}
