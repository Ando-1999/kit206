using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{

    class Researcher
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public string Title;
        public string Email;
        public Uri Photo;
        public DateTime StartInstitution;
        public DateTime StartCurrentJob;

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

        public override string ToString()
        {
            return $"{LastName}, {FirstName} ({Title})";
        }
    }
}
