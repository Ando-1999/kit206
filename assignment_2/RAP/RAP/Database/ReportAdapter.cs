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

        public static int fetchNumRecentPublications(Model.Researcher r)
        {
            MySqlDataReader rdr = null;
            int numPublications = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    $"SELECT COUNT(*) AS total FROM publication " +
                    $"WHERE doi IN " +
                    $"(" +
                        $"SELECT doi FROM researcher_publication " +
                        $"WHERE researcher_id=?id" +
                    $")" +
                    $"AND YEAR >= ?year",
                    conn);

                cmd.Parameters.AddWithValue("id", r.Id);
                // IMPORTANT: Database seems to be from around 2016.
                // This explicitly assumes that.
                // For an up-to-date database, replace 2016 with
                // DateTime.Now.Year
                cmd.Parameters.AddWithValue("year", 2016 - 3);

                rdr = cmd.ExecuteReader();
                rdr.Read();

                numPublications = (int)(Int64)rdr["total"];
                //numPublications = rdr.GetInt32(rdr.GetOrdinal("total"));
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

            return numPublications;
        }


        // Fetch list of staff from database.
        // TODO: Why did I write this? What is it for?
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
