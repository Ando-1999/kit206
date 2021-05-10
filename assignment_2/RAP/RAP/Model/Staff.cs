using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    class Staff : Researcher 
    {
        public EmploymentLevel Level;
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
            EmploymentLevel level = getEmploymentLevel();

            // Expected number of publications for each employment level.
            Dictionary<EmploymentLevel, double> expectedPublicationsByLevel = 
                new Dictionary<EmploymentLevel, double>() {
                    { EmploymentLevel.A, 0.5},
                    { EmploymentLevel.B, 1},
                    { EmploymentLevel.C, 2},
                    { EmploymentLevel.D, 3.2},
                    { EmploymentLevel.E, 4},
                };

            double expectedPublications = expectedPublicationsByLevel[level];

            return  threeYearAverage()/expectedPublications;
        }

        // Number of students currently or previously supervised.
        public int supervisions()
        {
            return 0;
        }

        // Employment level for current position.
        public EmploymentLevel getEmploymentLevel()
        {
            return EmploymentLevel.A;
        }

        // List of all positions ever occupied at institution.
        public List<Position> getPositions()
        {
            return null;
        }
    }
}
