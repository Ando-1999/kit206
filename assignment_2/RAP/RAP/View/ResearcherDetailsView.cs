using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.View
{
    class ResearcherDetailsView
    {
        public BasicDetailsTable details;
        public PreviousPositionsTable positions;
        public PublicationList publications;

        public ResearcherDetailsView()
        { 
        }

        // Get researcher.
        public Model.Researcher getResearcher()
        {
            return null;
        }

        // Get all of researcher's positions.
        public List<Model.Position> getPositions()
        {
            return null;
        }

        // Get all of researcher's publications.
        public List<Model.Publication> getPublications()
        {
            return null;
        }

        // Display table of researcher's cumulative publications by year.
        public CumulativePublicationsTable displayCumulativePublicationsTable()
        {
            return null;
        }
    }
}
