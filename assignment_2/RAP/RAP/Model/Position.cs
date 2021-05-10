using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{

    class Position
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private EmploymentLevel level;
        public EmploymentLevel Level
        {
            get { return level; }
            set { level = value; }
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        /*
        // Alternative implementation
        public static readonly Dictionary<EmploymentLevel, string> Title =
                new Dictionary<EmploymentLevel, string>()
                {
                    { EmploymentLevel.A, "Postdoc"},
                    { EmploymentLevel.B, "Lecturer"},
                    { EmploymentLevel.C, "Senior Lecturer"},
                    { EmploymentLevel.D, "Associate Professor"},
                    { EmploymentLevel.E, "Professor"},
                };
        */

        private static Dictionary<EmploymentLevel, string> title;
        public static Dictionary<EmploymentLevel, string> Title {
            get { return title; }
        }
        static Position()
        { 
            title = new Dictionary<EmploymentLevel, string>() {
                        { EmploymentLevel.A, "Postdoc"},
                        { EmploymentLevel.B, "Lecturer"},
                        { EmploymentLevel.C, "Senior Lecturer"},
                        { EmploymentLevel.D, "Associate Professor"},
                        { EmploymentLevel.E, "Professor"},
                    };
        }

        public Position()
        {
        }

        // Title of position.
        public string jobTitle()
        {
            return Title[Level];
        }
    }
}
