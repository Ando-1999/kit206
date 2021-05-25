using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    class Staff : Researcher 
    {
        // TODO: Is this necessary?
        // I can write it out by using Positions[0].Level
        // Will there ever be a time when that's unavailable?
        private EmploymentLevel level;
        public EmploymentLevel Level
        {
            get { return level; }
            set { level = value; }
        }
        private double performance;
        public double? Performance {
            get
            {
                // Performance not yet valid, so work it out
                // Could maybe use Performance.HasValue now
                if (performance < 0)
                    performance = getPerformance().Value;

                return performance;
            }
            set
            {
                if (Performance.HasValue)
                    performance = value.Value;
            }
        }

        public Staff()
        {
            // Invalid value to show it doesn't have any meaning yet.
            Performance = -1;
        }

        /*
         * Average number of publications authored in the last three whole
         * calendar years.
         */
        public double? threeYearAverage()
        {
            return Database.ReportAdapter.
                fetchNumRecentPublications(this)/(double?)3.0;
        }

        /* Three-year average divided by the expected number of publications
         * for employment level.
         */
        public double? getPerformance()
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

            double expectedPublications =
                expectedPublicationsByLevel[Positions[0].Level];

            return  (double?)threeYearAverage()/expectedPublications;
        }

        // Number of students currently or previously supervised.
        public int? supervisions()
        {
            return Database.ResearcherAdapter.fetchNumSupervisions(this);
        }

        public override string ToFullString()
        {
            return $"{Title} {FirstName} {LastName}\n" +
                $"{Positions[0].jobTitle()}\n";
        }
    }
}
