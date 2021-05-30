using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
            Publications = Database.PublicationAdapter.fetchPublicationsList(researcher);
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

    }
}
