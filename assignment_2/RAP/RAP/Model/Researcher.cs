using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Model
{
    public enum ResearcherType { NULL, STAFF, STUDENT };
    public enum ResearcherCampus
    {
        NULL,
        [Description("Hobart")]
        HOBART,
        [Description("Launceston")]
        LAUNCESTON,
        [Description("Cradle Coast")]
        CRADLE_COAST,
    };
    public enum ResearcherLevel { NULL, A, B, C, D };

    class Researcher : BaseModel
    {
        private static string TableName = "researcher";
        public int Id { get; set; }
        public ResearcherType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public ResearcherCampus Campus { get; set; }
        public string Email { get; set; }
        public Uri Photo { get; set; }
        //public string Photo { get; set; }
        public string Degree { get; set; }
        public int SupervisorId { get; set; }
        public ResearcherLevel Level { get; set; }

        public DateTime UtasStart { get; set; }
        public DateTime CurrentStart { get; set; }

        public DateTime StartInstitution;
        public DateTime StartCurrentJob;
        // abstract class Researcher
        // {
        // private int id;
        // public int Id
        // {
        //     get { return id; }
        //     set { id = value; }
        // }
        // private string firstName;
        // public string FirstName
        // {
        //     get { return firstName; }
        //     set { firstName = value; }
        // }
        // private string lastName;
        // public string LastName
        // {
        //     get { return lastName; }
        //     set { lastName = value; }
        // }
        // private string title;
        // public string Title
        // {
        //     get { return title; }
        //     set { title = value; }
        // }
        // private string email;
        // public string Email
        // {
        //     get { return email; }
        //     set { email = value; }
        // }
        // private Uri photo;
        // public Uri Photo
        // {
        //     get { return photo; }
        //     set { photo = value; }
        // }
        // private DateTime startInstitution;
        // public DateTime StartInstitution
        // {
        //     get { return startInstitution; }
        //     set { startInstitution = value; }
        // }
        private List<Model.Position> positions;
        public List<Model.Position> Positions
        {
            get { return positions; }
            set { positions = value; }
        }


        public static ResearcherType ParseResearcherType(string v)
        {
            if (v == null) return ResearcherType.NULL;

            if (v.Equals("Staff")) return ResearcherType.STAFF;
            if (v.Equals("Student")) return ResearcherType.STUDENT;

            return ResearcherType.NULL;

        }
        public static ResearcherCampus ParseResearcherCampus(string v)
        {
            if (v == null) return ResearcherCampus.NULL;

            if (v.Equals("Hobart")) return ResearcherCampus.HOBART;
            if (v.Equals("Launceston")) return ResearcherCampus.LAUNCESTON;
            if (v.Equals("Cradle Coast")) return ResearcherCampus.CRADLE_COAST;

            return ResearcherCampus.NULL;
        }
        public static ResearcherLevel ParseResearcherLevel(string v)
        {
            if (v == null) return ResearcherLevel.NULL;

            if (v.Equals("A")) return ResearcherLevel.A;
            if (v.Equals("B")) return ResearcherLevel.B;
            if (v.Equals("C")) return ResearcherLevel.C;
            if (v.Equals("C")) return ResearcherLevel.D;

            return ResearcherLevel.NULL;
        }

        public Researcher()
        {
            Positions = new List<Model.Position>();
        }

        // Title of currently held position.
        public string currentJobTitle()
        {
            if (Positions.Count == 0) return "N/A";
            return Positions[0].jobTitle();
        }

        // Starting date of currently held position.
        public DateTime commencedCurrentPosition()
        {
            if (Positions.Count == 0) return new DateTime(0);
            return Positions[0].StartDate;
        }

        // Total time with institution in fractional years.
        public double tenure()
        {
            if (Positions.Count == 0) return 0;
            DateTime end = Positions[0].EndDate;
            DateTime start = Positions[0].StartDate;
            if (end < start)
                end = DateTime.Now;
            TimeSpan span = end - start;
            double tenure = span.Days/365.0;
            return tenure;
        }

        // Total number of publications authored.
        public int numberOfPublications()
        {
            return Database.PublicationAdapter.totalPublications(this);
        }

        // Get all of researcher's publications.
        public List<Publication> getPublications()
        {
            return Database.PublicationAdapter.fetchPublicationsList(this);
        }

        // List of all positions ever occupied at institution.
        public List<Position> getPositions()
        {
            return Database.ResearcherAdapter.fetchPositions(this);
        }

        public override string ToString()
        {
            return $"{LastName}, {FirstName} ({Title})";
        }

        public static List<Researcher> All()
        {
            List<Researcher> researchers = new List<Researcher>();
            MySqlDataReader dr = DataReaderAll(TableName);
            if (dr != null)
            {
                while (dr.Read())
                {
                    researchers.Add(new Researcher
                    {
                        Id = GetInt32(dr, "id"),
                        Type = ParseResearcherType(GetString(dr, "type")),
                        FirstName = GetString(dr, "given_name"),
                        LastName = GetString(dr, "family_name"),
                        Title = GetString(dr, "title"),
                        Unit = GetString(dr, "unit"),
                        Campus = ParseResearcherCampus(GetString(dr, "campus")),

                        // nullable
                        Email = GetString(dr, "title"),
                        Photo = new Uri(GetString(dr, "photo")),
                        Degree = GetString(dr, "degree"),
                        SupervisorId = GetInt32(dr, "supervisor_id"),
                        Level = ParseResearcherLevel(GetString(dr, "level")),
                        UtasStart = DateTime.Parse(GetString(dr, "utas_start")),
                        CurrentStart = DateTime.Parse(GetString(dr, "current_start")),
                    });
                }
                connection.Close();
            }
            return researchers;
        }

        public List<Publication> publications()
        {
            var query = from rp in Model.ResearcherPublication.All()
                        join p in Model.Publication.All() on rp.Doi equals p.Doi
                        where rp.ResearcherId == 123460
                        select p;
            return query.ToList<Publication>();
        }


        //public override string ToString()
        //{
        //    return $"{Type}: {Degree} {FirstName} {LastName}, {Unit}, {Campus}";
        //}
        //public abstract string ToFullString();
    }
}
