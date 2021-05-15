using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Model
{
    public enum PublicationType { 
        NULL,
        [Description("Conference")]
        CONFERENCE,
        [Description("Journal")]
        JOURNAL,
        [Description("Other")]
        OTHER,
        };

    class Publication: BaseModel
    {
        public static string TableName = "publication";

        public string Doi { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public int Year { get; set; }
        public PublicationType Type { get; set; }
        public string CiteAs { get; set; }
        public DateTime Available { get; set; }

        //public DateTime PublicationDate { get; set; }
        //public PublicationType Type { get; set; }
        //public DateTime AvailabilityDate { get; set; }

        public Publication()
        {
        }

        // Number of days since publication became available.
        // May be negative if publication is not yet available.
        public int age()
        {
            return (DateTime.Now - Available).Days;
        }

        public static PublicationType ParsePublicationType(string v)
        {
            if (v == null) return PublicationType.NULL;

            if (v.Equals("Conference")) return PublicationType.CONFERENCE;
            if (v.Equals("Journal")) return PublicationType.JOURNAL;
            if (v.Equals("Other")) return PublicationType.OTHER;

            return PublicationType.NULL;
        }

        public static List<Publication> All()
        {
            List<Publication> researchers = new List<Publication>();
            MySqlDataReader dr = DataReaderAll(TableName);
            if (dr != null)
            {
                while (dr.Read())
                {
                    researchers.Add(new Publication
                    {
                        Doi = GetString(dr, "doi"),
                        Title = GetString(dr, "title"),
                        Authors = GetString(dr, "authors"),
                        Year = GetInt32(dr, "year"),
                        Type = ParsePublicationType(GetString(dr, "type")),
                        CiteAs = GetString(dr, "cite_as"),
                        Available = DateTime.Parse(GetString(dr, "available")),
                    });
                }
                connection.Close();
            }
            return researchers;
        }


        public override string ToString()
        {
            return $"Doi={Doi}, Title={Title}, Authors={Authors}, Year={Year}, Type={Type}, CiteAs={CiteAs}, Available={Available}";
        }
    }
}
