using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{

    abstract class Researcher
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private Uri photo;
        public Uri Photo
        {
            get { return photo; }
            set { photo = value; }
        }
        private DateTime startInstitution;
        public DateTime StartInstitution
        {
            get { return startInstitution; }
            set { startInstitution = value; }
        }
        private DateTime startCurrentJob;
        public DateTime StartCurrentJob
        {
            get { return startCurrentJob; }
            set { startCurrentJob = value; }
        }
        private List<Model.Position> positions;
        public List<Model.Position> Positions
        {
            get { return positions; }
            set { positions = value; }
        }


        public Researcher()
        {
            Positions = new List<Model.Position>();
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

        // List of all positions ever occupied at institution.
        public List<Position> getPositions()
        {
            return null;
        }

        public override string ToString()
        {
            return $"{LastName}, {FirstName} ({Title})";
        }
        public abstract string ToFullString();
    }
}
