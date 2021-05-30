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
        public string Doi { get; set; }

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

        private DateTime? publicationYear;
        public DateTime? PublicationYear { get; set; }

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

        public override string ToString()
        {
            return $"{PublicationYear.Value.Year} {Title}";
        }
    }
}
