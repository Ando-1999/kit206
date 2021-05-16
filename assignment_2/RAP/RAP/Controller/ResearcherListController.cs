using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Controller
{
    class ResearcherListController
    {

        // ???
        public List<string> filters;

        public ResearcherListController()
        {

        }

        public string currentjobTitle(Model.Researcher r)
        {
            if (r.GetType() == typeof(Model.Student))
                return "student";
            else
                return r.Positions[0].jobTitle();
        }


        // Load cumulative publications for reasearcher.
        public List<Model.Publication> loadCumulativePublications()
        {
            return null;
        }

        // Load basic details for all researchers.
        public List<string> loadResearcherList()
        {
            return null;
        }

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

        // Update researcher list to contain only those researchers with
        // details satisfying the current filters.
        public List<string> filterList()
        {
            return null;
        }

        // Load list of students a staff members is or has ever supervised.
        public List<string> loadSupervisions()
        {
            return null;
        }

        // Calculate cumulatie publications per year for a given researcher.
        public Dictionary<DateTime, int> calculateCumulativePublications()
        {
            return null;
        }

        // Load details of researcher.
        public Model.Researcher loadResearcher()
        {
            return null;
        }

        // Load name of researcher.
        public string loadResearcherName()
        {
            return null;
        }
    }
}
