using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RAP.Controller
{
    class ReportController
    {
        //private ReportType repType;
        //public ReportType RepType { get; set; }

        private List<Model.Staff> reportList;
        public List<Model.Staff> ReportList { get; set; }

        private ResearcherController researcherController;
        
        readonly Dictionary<ReportType, double> threshold =
            new Dictionary<ReportType, double>
            {
                    { ReportType.POOR, 0.0 },
                    { ReportType.BELOW_EXPECTATIONS, 0.7 },
                    { ReportType.MINIMUM_STANDARD, 1.1 },
                    { ReportType.STAR_PERFORMANCE, 2.0 }
            };
        

        public ReportController()
        {
            researcherController =
                Application.Current.FindResource("researcherController")
                as Controller.ResearcherController;

            //RepType = ReportType.NULL;
            ReportList = null;
        }

        // Request report for given performance level
        public void requestReport(ReportType type)
        {
            ReportList = new List<Model.Staff>();

            /*
            var query = from s in researcherController.ResearcherList
                        where s.GetType() == typeof(Model.Staff)
                        select s;
            */

            foreach (Model.Researcher r in researcherController.ResearcherList)
            {
                if (r.GetType() == typeof(Model.Staff))
                {
                    Model.Staff s =
                        Database.ResearcherAdapter.fetchResearcherDetails(r)
                        as Model.Staff;

                    double? performance =
                        researcherController.getPerformance(s);

                    if (!performance.HasValue)
                        continue;

                    ReportType repType =
                        performanceCategory(performance.Value);

                    if (repType == type)
                    {
                        ReportList.Add(
                            new Model.Staff
                            {
                                Id = s.Id,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Title = s.Title,
                                Email = s.Email,
                                Performance = performance.Value
                            }
                        );
                    }
                }
            }

            sortReport(type);
        }

        public void sortReport(ReportType type)
        {
            var sort = from r in ReportList
                       select r;
            switch (type)
            {
                case ReportType.POOR: case ReportType.BELOW_EXPECTATIONS:
                    // increaseing
                    sort = from r in ReportList
                           orderby r.Performance ascending
                           select r;
                    break;
                case ReportType.MINIMUM_STANDARD: case ReportType.STAR_PERFORMANCE:
                    // decreasing
                    sort = from r in ReportList
                           orderby r.Performance descending
                           select r;
                    break;
                default:
                    break;
            }

            ReportList = new List<Model.Staff>(sort);
        }

        // Copy emails of all researchers in report to clipboard
        public void copyEmails(List<Model.Researcher> rs)
        {
            List<string> emailList = Database.ReportAdapter.fetchResearcherEmails(rs);

            string emails = string.Join(" ", emailList);

            //Paste emails to clipboard
            Clipboard.SetText(emails);
        }

        // Classify performance into categories
        public ReportType performanceCategory(double performance)
        {
            ReportType repType = ReportType.NULL;

            if (performance <= threshold[ReportType.BELOW_EXPECTATIONS])
            {
                repType = ReportType.POOR;
            }
            else if (performance < threshold[ReportType.MINIMUM_STANDARD])
            {
                repType = ReportType.BELOW_EXPECTATIONS;
            }
            else if (performance < threshold[ReportType.STAR_PERFORMANCE])
            {
                repType = ReportType.MINIMUM_STANDARD;
            }
            else
            {
                repType = ReportType.STAR_PERFORMANCE;
            }

            return repType;
        }
    }
}
