using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.View
{
    class PublicationDetailsView
    {
        public string doi;
        public List<string> authors;
        public DateTime publicationYear;
        public PublicationType type;
        public string citeAs;
        public DateTime availabilityDate;
        public int age;

        public PublicationDetailsView()
        {
        }

        // ???
        public void setModel()
        { 
        }
    }
}
