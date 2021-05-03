using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Database
{
    // Static?
    class ERDAdapter
    {
        public ERDAdapter()
        {
        }

        // Fetch all of researcher's publications from database.
        public List<Model.Publication> fetchPublications(Model.Researcher r)
        {
            return null;
        }

        // Fetch list of researchers from database.
        public List<string> fetchResearcherList()
        {
            return null;
        }

        // Fetch list of students staff member is or has ever supervised
        // from database.
        public List<string> fetchSupervisions(Model.Staff s)
        {
            return null;
        }


        // Fetch publication from database.
        public Model.Publication fetchPublicationDetails()
        {
            return null;
        }


        // Fetch list of researcher emails from database.
        public List<string> fetchResearcherEmails(List<Model.Researcher> r)
        {
            return null;
        }

        // Fetch list of researcher's publications from database.
        public List<string> fetchPublicationList(Model.Researcher r)
        {
            return null;
        }

        // Fetch researcher from database.
        public Model.Researcher fetchResearcher()
        {
            return null;
        }

        // Fetch list of staff from database.
        public List<Model.Staff> fetchStaffList()
        {
            return null;
        }

        //???
        public string fetchResearcherName()
        {
            return null;
        }
    }
}
