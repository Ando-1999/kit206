using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace RAP.Controller
{
    public class PublicationController
    {
        private List<Model.Publication> publications;
        public List<Model.Publication> Publications { get; set; }

        private ObservableCollection<Model.Publication> publicationList;
        public ObservableCollection<Model.Publication> PublicationList { get; set; }

        private Model.Publication publicationDetails;
        public Model.Publication PublicationDetails { get; set; }

        public PublicationController()
        {
            Publications = null;
            PublicationList = new ObservableCollection<Model.Publication>();
            PublicationDetails = null;
        }

        // Number of days since publication became available.
        // May be negative if publication is not yet available.
        public int Age()
        {
            return (DateTime.Now - PublicationDetails.AvailabilityDate).Value.Days;
        }


        // Sort publications in order of increasing age, then in alphabetical order by title
        public void sortList()
        {
            var sort = Publications.OrderByDescending(p => p.PublicationYear)
                                   .ThenBy(p => p.Title);

            PublicationList.Clear();

            foreach (Model.Publication p in sort)
                PublicationList.Add(p);
        }

        // Filter list to include those only within a given year range.
        public List<string> filterByYearRange()
        {
            return null;
        }

        // Load basic details list of researcher's publications.
        public void loadPublicationList(Model.Researcher researcher)
        {
            Publications = Database.PublicationAdapter
                                   .fetchPublicationsList(researcher);
            sortList();
        }

        public ObservableCollection<Model.Publication> GetPublicationList()
        {
            return PublicationList;
        }

        public void GetPublicationDetails(Model.Publication publication)
        {
            PublicationDetails =
                Database.PublicationAdapter.fetchPublicationDetails(publication);

        }

        public Array GetCumulativeCount()
        {
            // Broup by year
            var group = from p in PublicationList
                        group p by p.PublicationYear;

            // Count distinct publications per year 
            var count = from g in @group
                        select new
                        {
                            Year = g.Key,
                            Count = (from c in g select c.Doi).Distinct().Count()
                        };

            // Count cumulative publicaitons by year
            // Can probably do this as a subquery of ocunt, but I think it'd be easier
            // to read this way
            var cumulativeCount =
                from c in count
                orderby c.Year
                select new
                {
                    Year = c.Year,
                    Count =
                    (
                        from n in count
                        where n.Year <= c.Year
                        select n.Count
                    ).Sum()
                };

            /*
            var ps = cumulativeCount.ToArray();

            string msg = "";
            foreach (var p in ps)
                msg += $"{p.Year.Value.Year}: {p.Count}\n";

            MessageBox.Show(msg);
            */

            return cumulativeCount.ToArray();
        }

    }
}
