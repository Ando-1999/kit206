using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Database
{
    static class PublicationAdapter
    {
        private static MySqlConnection conn;

        static PublicationAdapter()
        {
            conn = ERDAdapter.Conn;
        }

        // Fetch all of a researcher's publications from database.
        // DONE
        public static List<Model.Publication> fetchPublicationsList(Model.Researcher r)
        {

            MySqlCommand cmd = null;
            MySqlDataReader rdr = null;
            List<Model.Publication> publications = null;

            try
            {
                conn = ERDAdapter.Conn;
                conn.Open();
                cmd = new MySqlCommand(
                    "SELECT doi, title, year FROM publication " +
                    "WHERE doi IN" +
                    "(" +
                        "SELECT doi FROM researcher_publication " +
                        "WHERE researcher_id=?id" +
                    ")", conn);

                cmd.Parameters.AddWithValue("id", r.Id);

                rdr = cmd.ExecuteReader();

                publications = new List<Model.Publication>();

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
            }
            catch (MySqlException e)
            {
                ERDAdapter.Error("loading publications", e);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return publications;
        }

        // Fetch publication from database.
        // DONE
        public static Model.Publication fetchPublicationDetails(Model.Publication publication)
        {
            MySqlCommand cmd;
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                cmd = new MySqlCommand(
                    "SELECT authors, type, cite_as, available " +
                    "FROM publication WHERE doi=?doi", conn);

                cmd.Parameters.AddWithValue("doi", publication.Doi);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    publication.Authors = (string)rdr["authors"];
                    publication.Type = (PublicationType)Enum.Parse(
                        typeof(PublicationType), (string)rdr["type"]);
                    publication.CiteAs = (string)rdr["cite_as"];
                    publication.AvailabilityDate =
                        (DateTime)rdr["available"];
                }
            }
            catch (MySqlException e)
            {
                ERDAdapter.Error($"loading details for {publication.Doi}", e);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }

            return publication;
        }
    }
}
