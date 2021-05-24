using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Model
{
    class ResearcherPublication : BaseModel
    {
        public static string TableName = "researcher_publication";

        public int ResearcherId { get; set; }
        public string Doi { get; set; }

        public static List<ResearcherPublication> All()
        {
            List<ResearcherPublication> data = new List<ResearcherPublication>();
            MySqlDataReader dr = DataReaderAll(TableName);
            if (dr != null)
            {
                while (dr.Read())
                {
                    data.Add(new ResearcherPublication
                    {
                        ResearcherId = GetInt32(dr, "researcher_id"),
                        Doi = GetString(dr, "doi"),
                    });
                }
                connection.Close();
            }
            return data;
        }


        public override string ToString()
        {
            return $"ResearcherId={ResearcherId},Doi={Doi}";
        }
    }
}
