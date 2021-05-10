using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.View
{
    class BasicDetailsTable
    {
        public string name;
        public string title;
        public Campus campus;
        public string email;
        public Uri photo;
        public string currentJobTitle;
        public DateTime commencedEmployment;
        public DateTime commencedPosition;
        public double tenure;
        public int publications;

        // Staff only______________________
        public double threeYearAverage; // |
        public double performance;      // |
        public int supervisions;        // |
        // --------------------------------

        // Students only___________________
        public string degree;           // |
        public string supervisor;       // |
        // --------------------------------
    }
}
