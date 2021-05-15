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
        }

        // Title of currently held position.
        // Deduced from employment level/student status.
        public string currentJobTitle()
        {
            return null;
        }

        // Starting date of earliest position.
        public DateTime commencedWithInstitution()
        {
            DateTime ret = new DateTime();
            return ret;
        }

        // Starting date of currently held position.
        public DateTime commencedCurrentPosition()
        {
            DateTime ret = new DateTime();
            return ret;
        }

        // Total time with institution in fractional years.
        public double tenure()
        {
            return 0;
        }

        // Total number of publications authored.
        public int numberOfPublications()
        {
            return 0;
        }

        // Get all of researcher's publications.
        public List<Publication> getPublications()
        {
            return null;
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


        public override string ToString()
        {
            return $"{Type}: {Degree} {FirstName} {LastName}, {Unit}, {Campus}";
        }
    }
}
