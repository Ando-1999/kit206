using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Model
{
    //enum EmploymentLevel { A, B, C, D, E, Student }

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
        public string currentJobTitle()
        {
            return Positions[0].jobTitle();
        }

        // Starting date of currently held position.
        public DateTime commencedCurrentPosition()
        {
            return Positions[0].StartDate;
        }

        // Total time with institution in fractional years.
        public double tenure()
        {
            DateTime end = Positions[0].EndDate;
            DateTime start = Positions[0].StartDate;
            if (end < start)
                end = DateTime.Now;
            TimeSpan span = end - start;
            double tenure = span.Days/365.0;
            return tenure;
        }

        // Total number of publications authored.
        public int numberOfPublications()
        {
            return Database.PublicationAdapter.totalPublications(this);
        }

        // Get all of researcher's publications.
        public List<Publication> getPublications()
        {
            return Database.PublicationAdapter.fetchPublicationsList(this);
        }

        // List of all positions ever occupied at institution.
        public List<Position> getPositions()
        {
            return Database.ResearcherAdapter.fetchPositions(this);
        }

        public override string ToString()
        {
            return $"{LastName}, {FirstName} ({Title})";
        }
        public abstract string ToFullString();
    }
}
