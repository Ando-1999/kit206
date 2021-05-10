using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.View
{
    class ResearcherListView
    {
        public List<string> firstName;
        public List<string> lastName;
        public List<string> title;

        public ResearcherListView()
        { 
        }

        // Get names of all researchers in the format "lastName, firstName
        // (title)".
        public List<string> getResearcherNames()
        {
            return null;
        }

        // Filter names displayed by employment level/student status.
        public List<string> filterOnEmploymentLevel()
        {
            return null;
        }

        // Display details for selected researcher.
        public ResearcherDetailsView displayResearcherDetails()
        {
            return null;
        }
    }
}
