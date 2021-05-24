using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Controller
{
    class ResearcherController
    {
        public ResearcherController()
        { 
        }

        public ObservableCollection<Entity.Researcher> getResearchers()
        {
            return null;
        }

        // Request report for given performance level
        public void requestReport(ReportType type)
        { 
        }

        // Copy emails of all researchers in report to clipboard
        public void copyEmails()
        {
        }

        // ???
        public  List<string> filterByPerformance()
        {
            return null;
        }

        public void userSelectsResearcher(Model.Researcher r)
        {
            r.publications();
        }
    }
}
