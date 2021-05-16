using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Database
{
    class ResearcherAdapter
    {
        private static MySqlConnection conn;

        static ResearcherAdapter()
        {
            conn = ERDAdapter.Conn;
        }

        // Fetch list of researchers from database.
        // DONE
        public static List<Model.Researcher> fetchResearcherList()
        {
            MySqlCommand cmd = null;
            MySqlDataReader rdr = null;
            List<Model.Researcher> researchers = null;
            try
            {
                conn.Open();

                cmd = new MySqlCommand(
                    "SELECT id, family_name, given_name, title, type " +
                    "FROM researcher", conn);

                rdr = cmd.ExecuteReader();

                researchers = new List<Model.Researcher>();

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
            }
            catch (MySqlException e)
            {
                ERDAdapter.Error("loading researchers", e);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return researchers;
        }

        // Assumes Researcher object already contains Id, FirstName,
        // LastName, and title
        // DONE
        public static Model.Researcher fetchResearcherDetails(Model.Researcher r)
        {

            MySqlCommand cmd = null;
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                cmd = new MySqlCommand(
                    "SELECT * FROM researcher WHERE id=?id", conn);

                cmd.Parameters.AddWithValue("id", r.Id);

                rdr = cmd.ExecuteReader();

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
            }
            catch (MySqlException e)
            {
                ERDAdapter.Error("loading details for " +
                    $"{r.Title} {r.FirstName} {r.LastName}", e);
            }
            finally 
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return r;
        }

        // Fetch list of students staff member is or has ever supervised
        // from database.
        // DONE
        public static List<Model.Student> fetchSupervisions(Model.Staff s)
        {
            MySqlCommand cmd;
            MySqlDataReader rdr = null;
            List<Model.Student> supervisions = null;
            try
            {
                conn.Open();

                cmd = new MySqlCommand(
                    "SELECT * FROM researcher " +
                    "WHERE supervisor_id=?id", conn);

                cmd.Parameters.AddWithValue("id", s.Id);

                rdr = cmd.ExecuteReader();

                supervisions = new List<Model.Student>();

                while (rdr.Read())
                {
                    supervisions.Add(new Model.Student
                    {
                        Id = (int)rdr["id"],
                        FirstName = (string)rdr["given_name"],
                        LastName = (string)rdr["family_name"],
                        Title = (string)rdr["title"],
                        Degree = (string)rdr["degree"]
                    });
                }
            }
            catch (MySqlException e)
            {
                ERDAdapter.Error("loading supervisions for " +
                    $"{s.Title} {s.FirstName} {s.LastName}", e); 
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return supervisions;
        }
        // Fetch list of students staff member is or has ever supervised
        // from database.
        // DONE
        public static int fetchNumSupervisions(Model.Staff s)
        {
            MySqlDataReader rdr = null;
            int numSupervisions = 0;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM researcher " +
                    "WHERE supervisor_id=?id", conn);

                cmd.Parameters.AddWithValue("id", s.Id);

                rdr = cmd.ExecuteReader();

                rdr.Read();
                numSupervisions = rdr.GetInt32(0);

            }
            catch (MySqlException e)
            {
                ERDAdapter.Error("loading supervisions for " +
                    $"{s.Title} {s.FirstName} {s.LastName}", e);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return numSupervisions;
        }

    }
}
