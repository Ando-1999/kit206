using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Database
{
    abstract class ReportAdapter : ERDAdapter
    {

        static ReportAdapter()
        {
        }

        // Fetch list of researcher emails from database.
        public static List<string> fetchResearcherEmails(List<Model.Researcher> researchers)
        {
            MySqlCommand cmd;
            
            List<string> emails = null;
            try
            {
                Conn.Open();

                cmd = new MySqlCommand(
                    "SELECT email FROM researcher " +
                    "WHERE FIND_IN_SET(id, ?ids) != 0", Conn);

                var filter = from r in researchers
                             select r.Id;
                List<int?> researcherIds = new List<int?>(filter);

                cmd.Parameters.AddWithValue("ids",
                    String.Join(",", researcherIds));

                Rdr = cmd.ExecuteReader();

                emails = new List<string>();

                while (Rdr.Read())
                    emails.Add(GetString("email"));
            }
            catch (MySqlException e)
            {
                Error("loading emails", e);
            }
            finally
            {
                if (Conn != null) Conn.Close();
                if (Rdr != null)
                {
                    Rdr.Close();
                    Rdr = null;
                }
            }

            return emails;
        }

        // Fetch number of publications for reasearcher in last three
        // calendar years.
        // IMPORTANT: assumes a current date of 2016 to conform with database
        public static int? fetchNumRecentPublications(Model.Researcher r)
        {
            int? numPublications = null;
            try
            {
                Conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    $"SELECT COUNT(*) AS total FROM publication " +
                    $"WHERE doi IN " +
                    $"(" +
                        $"SELECT doi FROM researcher_publication " +
                        $"WHERE researcher_id=?id" +
                    $")" +
                    $"AND YEAR >= ?year",
                    Conn);

                cmd.Parameters.AddWithValue("id", r.Id);
                // IMPORTANT: Database seems to be from around 2016.
                // This explicitly assumes that.
                // For an up-to-date database, replace 2016 with
                // DateTime.Now.Year
                cmd.Parameters.AddWithValue("year", 2016 - 3);

                Rdr = cmd.ExecuteReader();
                Rdr.Read();

                numPublications = GetInt64("total");
            }
            catch (MySqlException e)
            {
                Error($"loading number of recent publicaitons for {r.FirstName}" +
                    $"{r.LastName}", e);
            }
            finally
            {
                if (Conn != null) Conn.Close();
                if (Rdr != null)
                {
                    Rdr.Close();
                    Rdr = null;
                }
            }

            return numPublications;
        }


        // Fetch list of staff from database.
        // TODO: Why did I write this? What is it for?
        public static List<Model.Staff> fetchStaffList()
        {
            List<Model.Staff> staff = null;
            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM researcher " +
                    "WHERE type='Staff'", Conn);

                Rdr = cmd.ExecuteReader();

                staff = new List<Model.Staff>();

                while (Rdr.Read())
                {
                    staff.Add(new Model.Staff
                    {
                        Id = GetInt("id"),
                        FirstName = GetString("given_name"),
                        LastName = GetString("family_name"),
                        Title = GetString("title"),
                        Email = GetString("email")
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("loading staff list", e);
            }
            finally
            {
                if (Conn != null) Conn.Close();
                if (Rdr != null)
                {
                    Rdr.Close();
                    Rdr = null;
                }
            }

            return staff;
        }

        // Return list of researchers by their performance level
        // Can this be done without loading too much data into memory?
        // i.e. with an SQL query?
        // TODO
        public static List<Model.Researcher>
            fetchResearchersByPerformance(ReportType t)
        {
            Dictionary<EmploymentLevel, double> performance =
                new Dictionary<EmploymentLevel, double>()
                {
                    { EmploymentLevel.A, 0.5 * 3},
                    { EmploymentLevel.B, 1 * 3},
                    { EmploymentLevel.C, 2 * 3},
                    { EmploymentLevel.D, 3.2 * 3},
                    { EmploymentLevel.E, 4 * 3},
                };

            Dictionary<ReportType, double> lowerBound =
                new Dictionary<ReportType, double>
                {
                    { ReportType.POOR, 0.0 },
                    { ReportType.BELOW_EXPECTATIONS, 0.7 },
                    { ReportType.MINIMUM_STANDARD, 1.1 },
                    { ReportType.STAR_PERFORMANCE, 2.0 }
                };
            try
            {
                Conn.Open();

                //double perf = performance[t];


                MySqlCommand cmd = new MySqlCommand(
                    "SELECT title, given_name, family_name FROM researcher " +
                    "WHERE ",
                    Conn);
            }
            catch (MySqlException e)
            {
                Error("loading report", e);
            }
            finally
            {
                if (Conn != null) Conn.Close();
                if (Rdr != null)
                {
                    Rdr.Close();
                    Rdr = null;
                }
            }
            return null;
        }
    }
}
