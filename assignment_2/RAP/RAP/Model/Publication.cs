using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{

    public class Publication
    {

        private string doi;
        public string Doi
        {
            get { return doi; }
            set { doi = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string authors;
        public string Authors
        {
            get { return authors; }
            set { authors = value; }
        }

        private DateTime publicationYear;
        public DateTime? PublicationYear
        {
            get {
                if (PublicationYear.HasValue)
                    return publicationYear;
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    publicationYear = value.Value;
            }
        }

        private PublicationType type;
        public PublicationType Type
        {
            get { return type; }
            set { type = value; }
        }

        private string citeAs;
        public string CiteAs
        {
            get { return citeAs; }
            set { citeAs = value; }
        }

        private DateTime? availabilityDate;
        public DateTime? AvailabilityDate { get; set; }

        public Publication()
        {
        }

        // Number of days since publication became available.
        // May be negative if publication is not yet available.
        // TODO: move to controller
        public int age()
        {
            return (DateTime.Now - AvailabilityDate).Value.Days;
        }

        public override string ToString()
        {
            return $"{PublicationYear.Value.Year} {Title}";
        }
        public string ToFullString()
        {
            return $"{Doi}\n" + 
                $"{Title}\n" +
                $"{Authors}\n" +
                $"{PublicationYear.Value.Year}\n" +
                $"{Type}\n" +
                $"{CiteAs}\n" +
                $"{AvailabilityDate.Value.ToShortDateString()}\n" +
                $"{age()}";
        }
    }
}
