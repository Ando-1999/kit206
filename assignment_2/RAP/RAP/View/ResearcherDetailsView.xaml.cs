using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RAP.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ResearcherDetailsView : Window
    {
        // researcherList controller
        Controller.ResearcherController researcherController =
            new Controller.ResearcherController();

        Model.Researcher researcher;



        public ResearcherDetailsView(string id)
        {
            InitializeComponent();


            /*

            researcher = researcherController.loadResearcher(id);

            label_familyName.Content = researcher.FirstName;
            label_givenName.Content = researcher.LastName;
            label_title.Content = researcher.Title;
            label_email.Content = researcher.Email;
            label_currentJobTitle.Content = researcher.currentJobTitle();
            label_campus.Content = researcher.Campus;
            //label_schoolOrUnit.Content = researcher;
            label_tenure.Content = researcher.tenure();
            label_commencedInstitution.Content = researcher.commencedCurrentPosition();
            label_commencedCurrentPosition.Content = researcher.commencedCurrentPosition();
            //label_threeYearAverage.Content = researcher;
            label_degree.Content = researcher.Degree;
            //label_performance.Content = researcher;
            label_supervisor.Content = researcher.SupervisorId; // TODO: supervisor
            //label_supervisons.Content = researcher.;
            */

        }


        //public BasicDetailsTable details;
        //public PreviousPositionsTable positions;
        //public PublicationList publications;

        //public ResearcherDetailsView()
        //{
        //}

        //// Get researcher.
        //public Model.Researcher getResearcher()
        //{
        //    return null;
        //}

        //// Get all of researcher's positions.
        //public List<Model.Position> getPositions()
        //{
        //    return null;
        //}

        //// Get all of researcher's publications.
        //public List<Model.Publication> getPublications()
        //{
        //    return null;
        //}

        //// Display table of researcher's cumulative publications by year.
        //public CumulativePublicationsTable displayCumulativePublicationsTable()
        //{
        //    return null;
        //}

    }
}
