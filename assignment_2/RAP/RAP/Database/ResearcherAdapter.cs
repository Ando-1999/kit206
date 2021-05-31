using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace RAP.Database
{
    abstract class ResearcherAdapter : ERDAdapter
    {

        static ResearcherAdapter()
        {
        }

        // Fetch basic details of researchers from database.
        public static List<Model.Researcher> fetchResearcherList()
        {
            List<Model.Researcher> researchers = null;

            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT id, family_name, given_name, title, type, level " +
                    "FROM researcher", Conn);

                Rdr = cmd.ExecuteReader();

                researchers = new List<Model.Researcher>();

                while (Rdr.Read())
                {
                    Model.Researcher researcher = null;
                    switch (GetEnum<ResearcherType>("type"))
                    {
                        case ResearcherType.STAFF:
                            researcher = new Model.Staff();
                            ((Model.Staff)researcher).Level =
                                GetEnum<EmploymentLevel>("level");
                            break;
                        case ResearcherType.STUDENT:
                            researcher = new Model.Student();
                            break;
                        default:
                            break;
                    }
                    researcher.Id = GetInt("id");
                    researcher.FirstName = GetString("given_name");
                    researcher.LastName = GetString("family_name");
                    researcher.Title = GetString("title");
                    researchers.Add(researcher);
                }
            }
            catch (MySqlException e)
            {
                Error("loading researchers", e);
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

            return researchers;
        }

        // Assumes Researcher object already contains Id, FirstName,
        // LastName, and title
        public static Model.Researcher fetchResearcherDetails(Model.Researcher researcher)
        {
            Model.Researcher r = null;
            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM researcher WHERE id=?id", Conn);

                cmd.Parameters.AddWithValue("id", researcher.Id);

                Rdr = cmd.ExecuteReader();

                Rdr.Read();

                switch (GetEnum<ResearcherType>("type"))
                {
                    case ResearcherType.STAFF:
                        r = new Model.Staff();
                        r.Positions.Add(new Model.Position {

                            Level = GetEnum<EmploymentLevel>("level"),

                            StartDate = GetDateTime("current_start"),

                            EndDate = null
                        }); ;
                        break;
                    case ResearcherType.STUDENT:
                        r = new Model.Student();
                        r.Positions.Add(new Model.Position {

                            Level = EmploymentLevel.STUDENT,

                            StartDate = GetDateTime("current_start"),

                            EndDate = null
                        }); 

                        ((Model.Student)r).Degree = GetString("degree");

                        ((Model.Student)r).SupervisorId =
                            GetInt("supervisor_id");
                        break;
                    default:
                        return null;
                }

                r.Id = researcher.Id;
                r.FirstName = researcher.FirstName;
                r.LastName = researcher.LastName;
                r.Title = researcher.Title;
                r.Email = GetString("email");
                r.Photo = GetUri("photo");
                r.StartInstitution = GetDateTime("utas_start");
                r.Unit = GetString("unit");
                r.Campus = GetEnum<Campus>("campus");
            }
            catch (MySqlException e)
            {
                Error("loading details for " +
                    $"{researcher.Title} {researcher.FirstName} {researcher.LastName}", e);

                return null;
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

            // Fetch any positions from the positions table
            fetchPositions(r);

            return r;
        }

        // Fetch student's supervisor
        // Assumes Staff object already contains Id
        // TODO: Only fetch basic details?
        public static Model.Staff fetchSupervisor(Model.Staff s)
        {
            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM researcher WHERE id=?id", Conn);

                cmd.Parameters.AddWithValue("id", s.Id);

                Rdr = cmd.ExecuteReader();

                Rdr.Read();

                s.FirstName = GetString("given_name");
                s.LastName = GetString("family_name");
                s.Title = GetString("title");
                s.Positions.Add(new Model.Position
                {
                    Level = GetEnum<EmploymentLevel>("level"),

                    StartDate = GetDateTime("current_start"),
                });

                s.Email = GetString("email");
                s.Photo = GetUri("photo");
                s.StartInstitution = GetDateTime("utas_start");
            }
            catch (MySqlException e)
            {
                Error("loading details for " +
                    $"{s.Title} {s.FirstName} {s.LastName}", e);
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

            return s;
        }

        // Fetch list of students staff member is or has ever supervised
        // from database.
        public static List<Model.Student> fetchSupervisions(Model.Staff s)
        {
            List<Model.Student> supervisions = null;

            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM researcher " +
                    "WHERE supervisor_id=?id", Conn);

                cmd.Parameters.AddWithValue("id", s.Id);

                Rdr = cmd.ExecuteReader();

                supervisions = new List<Model.Student>();

                while (Rdr.Read())
                {
                    supervisions.Add(new Model.Student
                    {
                        Id = GetInt("id"),
                        FirstName = GetString("given_name"),
                        LastName = GetString("family_name"),
                        Title = GetString("title"),
                        Degree = GetString("degree"),
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("loading supervisions for " +
                    $"{s.Title} {s.FirstName} {s.LastName}", e); 
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

            return supervisions;
        }

        // Fetch list of students staff member is or has ever supervised
        // from database.
        public static int? fetchNumSupervisions(Model.Staff s)
        {
            
            int? numSupervisions = 0;
            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) AS supervisions FROM researcher " +
                    "WHERE supervisor_id=?id", Conn);

                cmd.Parameters.AddWithValue("id", s.Id);

                Rdr = cmd.ExecuteReader();

                Rdr.Read();
                numSupervisions = GetInt64("supervisions");
            }
            catch (MySqlException e)
            {
                Error("loading supervisions for " +
                    $"{s.Title} {s.FirstName} {s.LastName}", e);
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

            return numSupervisions;
        }
        // Fetch list of students staff member is or has ever supervised
        // from database.
        public static List<Model.Position> fetchPositions(Model.Researcher r)
        {
            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT * FROM position " +
                    "WHERE id=?id " +
                    "ORDER BY start DESC", Conn);

                cmd.Parameters.AddWithValue("id", r.Id);

                Rdr = cmd.ExecuteReader();
                // Skip current position, as that was created when the
                // researcher's details were loaded
                if (Rdr.Read() && GetDateTime("start") == r.Positions[0].StartDate)
                    r.Positions[0].EndDate = GetDateTime("end");

                while (Rdr.Read())
                {
                    /*
                    // I don't believe students can appear in this table,
                    // but if they can, this will be needed
                    switch (ERDAdapter.type[r.GetType()])
                    {
                        case ResearcherType.Staff:
                            break;
                        case ResearcherType.Student:
                            break;
                        default;
                            break;
                    }
                    */

                    r.Positions.Add(new Model.Position {
                        Level = GetEnum<EmploymentLevel>("level"),

                        StartDate = GetDateTime("start"),

                        // Since current position is skipped, NULL is not
                        // a possible return type
                        EndDate = GetDateTime("end"),
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("loading past positions for " +
                    $"{r.Title} {r.FirstName} {r.LastName}", e);
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

            return r.Positions;
        }
    }
}
