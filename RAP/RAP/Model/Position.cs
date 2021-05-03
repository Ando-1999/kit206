using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{

    class Position
    {
        public string name;
        public EmploymentLevel level;
        public DateTime startDate;
        public DateTime endDate;

        public static Dictionary<EmploymentLevel, string> titles =
            new Dictionary<EmploymentLevel, string>()
            {
                { EmploymentLevel.A, "Postdoc"},
                { EmploymentLevel.B, "Lecturer"},
                { EmploymentLevel.C, "Senior Lecturer"},
                { EmploymentLevel.D, "Associate Professor"},
                { EmploymentLevel.E, "Professor"},
            };

        public Position()
        { 
        }

        // Title of position.
        public string jobTitle()
        {
            return null;
        }
    }
}
