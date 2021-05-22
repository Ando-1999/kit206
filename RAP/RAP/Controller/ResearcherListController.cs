using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Required for making the 'viewable' variables for manipulation
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Controller
{
    class ResearcherListController
    {

        /// ???
        public List<string> filters;

        private List<Model.Researcher> researcher;
        public List<Model.Researcher> Researchers { get { return researcher; } set { } } //Was Workers

        ///Essentially, this allows for manipulation of pulled data without actually modifying the DB
        private ObservableCollection<Model.Researcher> viewableResearchers;
        public ObservableCollection<Model.Researcher> VisibleResearchers { get { return viewableResearchers; } set { } }

        public ResearcherListController()
        {
            researcher = Database.ResearcherAdapter.fetchResearcherList();
            viewableResearchers = new ObservableCollection<Model.Researcher>(researcher);

            //How to handle the publications for a researcher?
        }

        /// <summary>
        /// GetViewableList
        /// Returns the list of researchers for display
        /// </summary>
        public ObservableCollection<Model.Researcher> GetViewableList()
        {
            return VisibleResearchers;
        }

        // Title of currently occupied position
        public string currentjobTitle(Model.Researcher r)
        {
            if (r.GetType() == typeof(Model.Student))
                return "Student";
            else
                return r.Positions[0].jobTitle();
        }


        // Load cumulative publications for reasearcher.
        public List<Model.Publication> loadCumulativePublications()
        {
            return null;
        }

        /// Blake's Comment: Probably not necessary, as we use the adapter to fetch the research list
        /// Not removing for your opinion
        // Load basic details for all researchers.
        public List<string> loadResearcherList()
        {
            return null;
        }

        /// Blake's Comment: You've done this in the Researcher model
        // Format list of researchers to be of the form "firstName, lastName
        // (title)".
        public List<string> formatList()
        {
            return null;
        }

        // Update filters (employment status, first name partial match,
        // last name partial match) based on user input.
        public void updateFilters()
        {
        }

        /// Blake's Comment: Wouldn't we need two filters for the level and type?
        // Update researcher list to contain only those researchers with
        // details satisfying the current filters.
        public List<string> filterList()
        {
            return null;
        }

        /// Blake's Comment: Same as loadResearcher
        /// <returns></returns>
        // Load list of students a staff members is or has ever supervised.
        public List<string> loadSupervisions()
        {
            return null;
        }

        /// Blake's Comment: Now, you call something similar in PublicationAdapter (totalPublications)
        /// So, do we want this in publications, or here?
        // Calculate cumulatie publications per year for a given researcher.
        public Dictionary<DateTime, int> calculateCumulativePublications()
        {
            return null;
        }

        /// Blake's Comment: Same as loadResearcher
        // Load details of researcher.
        public Model.Researcher loadResearcher()
        {
            return null;
        }

        /// Blake's Comment: Same as loadResearcher
        // Load name of researcher.
        public string loadResearcherName()
        {
            return null;
        }
    }
}
