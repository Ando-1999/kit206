using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Database
{
    class ReportAdapter
    {
        private static MySqlConnection conn;
        static ReportAdapter()
        {
            conn = ERDAdapter.Conn;
        }

        // Fetch list of researcher emails from database.
        // DONE
        public static List<string> fetchResearcherEmails(List<Model.Researcher> researchers)
        {
            MySqlCommand cmd;
            MySqlDataReader rdr = null;
            List<string> emails = null;
            try
            {
                conn.Open();

                cmd = new MySqlCommand(
                    "SELECT email FROM researcher " +
                    "WHERE FIND_IN_SET(id, ?ids) != 0", conn);

                var filter = from r in researchers
                             select r.Id;

                List<int> researcherIds = new List<int>(filter);

                cmd.Parameters.AddWithValue("ids", String.Join(",", researcherIds));

                rdr = cmd.ExecuteReader();

                emails = new List<string>();

                while (rdr.Read())
                    emails.Add((string)rdr["email"]);
            }
            catch (MySqlException e)
            {
                ERDAdapter.Error("loading emails", e);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return emails;
        }

        // Fetch list of staff from database.
        // TODO: Unimportant?
        // DONE
        public static List<Model.Staff> fetchStaffList()
        {
            MySqlCommand cmd;
            MySqlDataReader rdr = null;
            List<Model.Staff> staff = null;
            try
            {
                conn.Open();

                cmd = new MySqlCommand(
                    "SELECT * FROM researcher " +
                    "WHERE type='Staff'", conn);

                rdr = cmd.ExecuteReader();

                staff = new List<Model.Staff>();

                while (rdr.Read())
                {
                    staff.Add(new Model.Staff
                    {
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
            }
            catch (MySqlException e)
            {
                ERDAdapter.Error("loading staff list", e);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return staff;
        }
    }
}
