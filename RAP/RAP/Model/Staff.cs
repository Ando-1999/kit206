using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    class Staff : Researcher 
    {
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
                new Dictionary<EmploymentLevel, double>();
            expectedPublicationsByLevel.Add(EmploymentLevel.A, 0.5);
            expectedPublicationsByLevel.Add(EmploymentLevel.B, 1);
            expectedPublicationsByLevel.Add(EmploymentLevel.C, 2);
            expectedPublicationsByLevel.Add(EmploymentLevel.D, 3.2);
            expectedPublicationsByLevel.Add(EmploymentLevel.E, 4);

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
