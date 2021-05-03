using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{

    class Publication
    {
        public string doi;
        public string title;
        public DateTime publicationDate;
        public PublicationType type;
        public string citeAs;
        public DateTime availabilityDate;

        public Publication()
        {
        }

        // Number of days since publication became available.
        // May be negative if publication is not yet available.
        public int age()
        {
            return 0;
        }
    }
}
