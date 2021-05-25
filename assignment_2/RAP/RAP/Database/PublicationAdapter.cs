using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RAP.Database
{
    abstract class PublicationAdapter : ERDAdapter
    {

        static PublicationAdapter()
        {
        }

        // Fetch all of a researcher's publications from database.
        public static List<Model.Publication> fetchPublicationsList(Model.Researcher r)
        {
            List<Model.Publication> publications = null;

            try
            {
                Conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT doi, title, year FROM publication " +
                    "WHERE doi IN" +
                    "(" +
                        "SELECT doi FROM researcher_publication " +
                        "WHERE researcher_id=?id" +
                    ")", Conn);

                cmd.Parameters.AddWithValue("id", r.Id);

                Rdr = cmd.ExecuteReader();

                publications = new List<Model.Publication>();

                while (Rdr.Read())
                {
                    // Add publication from database to list
                    publications.Add(new Model.Publication
                    {
                        Doi = GetString("doi"),
                        Title = GetString("title"),
                        PublicationYear = GetYear("year")
                    });
                }
            }
            catch (MySqlException e)
            {
                Error("loading publications", e);
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

            return publications;
        }

        // Fetch publication from database.
        // DONE
        public static Model.Publication fetchPublicationDetails(Model.Publication publication)
        {
            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT authors, type, cite_as, available " +
                    "FROM publication WHERE doi=?doi", Conn);

                cmd.Parameters.AddWithValue("doi", publication.Doi);

                Rdr = cmd.ExecuteReader();

                while (Rdr.Read())
                {
                    publication.Authors = GetString("authors");
                    publication.Type = GetEnum<PublicationType>("type");
                    publication.CiteAs = GetString("cite_as");
                    publication.AvailabilityDate = GetDateTime("available");
                }
            }
            catch (MySqlException e)
            {
                Error($"loading details for {publication.Doi}", e);
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

            return publication;
        }
        // Total number of publications authored by researcher
        public static int? totalPublications(Model.Researcher r)
        {
            int? publications = null;
            try
            {
                Conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    $"SELECT COUNT(*) AS total FROM researcher_publication " +
                    $"WHERE researcher_id=?id",
                    Conn);

                cmd.Parameters.AddWithValue("id", r.Id);

                Rdr = cmd.ExecuteReader();

                Rdr.Read();

                publications = GetInt64("total");
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

            return publications;
        }
    }
}
