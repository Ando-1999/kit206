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
using System.Reflection;

namespace RAP.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ResearcherDetailsView : Window
    {
        public ResearcherDetailsView(Controller.ResearcherController researcherController)
        {
            this.DataContext = researcherController.ResearcherDetails;

            InitializeComponent();
        }

        private void button_viewPublications_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CulmativeCountView();
            window.Show();
        }

        private void button_viewSupervisons_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SupervisionsView();
            window.Show();
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
