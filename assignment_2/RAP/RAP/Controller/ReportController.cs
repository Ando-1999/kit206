using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Controller
{
    class ReportController
    {
        public ReportController()
        { 
        }

        // Request report for given performance level
        public void requestReport(ReportType type)
        { 

        }

        // Copy emails of all researchers in report to clipboard
        // TODO: can't access clipboard as console application
        public void copyEmails(List<Model.Researcher> rs)
        {
            List<string> emailList = Database.ReportAdapter.fetchResearcherEmails(rs);

            string emails = string.Join(" ", emailList);

            //Paste emails to clipboard
            //Clipboard.SetText(emails);
        }

        // ???
        public  List<string> filterByPerformance()
        {
            return null;
        }
    }
}
