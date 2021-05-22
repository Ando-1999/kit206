using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP_WPF.Model
{

    class Position
    {
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
        // TODO: could instantiate here and make const/readonly
        private static Dictionary<EmploymentLevel, string> title;
        public static Dictionary<EmploymentLevel, string> Title {
            get { return title; }
            set { title = value; }
        }
        static Position()
        {
            Title = new Dictionary<EmploymentLevel, string>() {
                        { EmploymentLevel.A, "Postdoc"},
                        { EmploymentLevel.B, "Lecturer"},
                        { EmploymentLevel.C, "Senior Lecturer"},
                        { EmploymentLevel.D, "Associate Professor"},
                        { EmploymentLevel.E, "Professor"},
                        { EmploymentLevel.Student, "Student"},
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

        public override string ToString()
        {
            string end = EndDate.ToShortDateString();

            if (EndDate < StartDate) end = "present";

            return $"{jobTitle()}: {StartDate.ToShortDateString()} - {end}";
        }
    }
}
