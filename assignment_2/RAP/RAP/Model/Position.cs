using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
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
        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                if (value != null)
                    startDate = (DateTime)value;
            }
        }
        private DateTime endDate;
        public DateTime? EndDate
        {
            get
            {
                if (EndDate.HasValue)
                    return endDate;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    endDate = (DateTime)value;
            }
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
            string start;
            string end;
            if (StartDate.HasValue)
                start = StartDate.Value.ToShortDateString();
            else
                start = "present";

            if (EndDate.HasValue)
                end = endDate.ToShortDateString();
            else
                end = "N/A";

            return $"{jobTitle()}: {start} - {end}";
        }
    }
}
