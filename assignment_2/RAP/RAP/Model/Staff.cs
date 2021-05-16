using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    class Staff : Researcher 
    {
        private EmploymentLevel level;
        public EmploymentLevel Level
        {
            get { return level; }
            set { level = value; }
        }
        public Staff()
        { 
        }

        /*
         * Total number of publications authored in the past 3 whole calendar
         * years divided by 3.
         */
        public double threeYearAverage()
        {
            return 0;
        }

        /* Three-year average divided by the expected number of publications
         * for employment level.
         */
        public double performance()
        {
            // Expected number of publications for each employment level.
            Dictionary<EmploymentLevel, double> expectedPublicationsByLevel = 
                new Dictionary<EmploymentLevel, double>() {
                    { EmploymentLevel.A, 0.5},
                    { EmploymentLevel.B, 1},
                    { EmploymentLevel.C, 2},
                    { EmploymentLevel.D, 3.2},
                    { EmploymentLevel.E, 4},
                };

            double expectedPublications = expectedPublicationsByLevel[Level];

            return  threeYearAverage()/expectedPublications;
        }

        // Number of students currently or previously supervised.
        public int supervisions()
        {
            return 0;
        }

        public override string ToFullString()
        {
            return $"{Title} {FirstName} {LastName}\n" +
                $"{Level}\n";
        }
    }
}
