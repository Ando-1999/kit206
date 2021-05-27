using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    public class Staff : Researcher 
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
        /*
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
        */

        public Staff()
        {
            // Invalid value to show it doesn't have any meaning yet.
            //Performance = -1;
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
