using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.View
{
    class PerformanceReportView
    {
        public List<double> performance;
        public List<string> name;

        public PerformanceReportView()
        {
        }

        // Order researchers by performance.
        // In order of increasing performance for POOR and
        // BELOW_EXPECTATIONS.
        // In order of decreasing performance for MINIMUM_STANDARD
        // and STAR_PERFORMANCE.
        public void orderList()
        { 
        }
    }
}
