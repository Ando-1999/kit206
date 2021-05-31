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
        private Controller.PublicationController publicationController;

        public ResearcherDetailsView(Controller.ResearcherController researcherController)
        {
            this.DataContext = researcherController.ResearcherDetails;

            publicationController = (Controller.PublicationController)
                Application.Current
                           .FindResource("publicationController");

            publicationController.loadPublicationList(researcherController.ResearcherDetails);
            InitializeComponent();

        }

        private void viewCumulativePublications(object sender, RoutedEventArgs e)
        {
            publicationController.GetCumulativeCount();

            Window window = new CumulativePublicationsView();
            window.Show();
        }

        private void button_viewSupervisons_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SupervisionsView();
            window.Show();
        }

        private void ViewPublicationDetails(object sender, SelectionChangedEventArgs e)
        {
            // Update PublicationDetails with selected researcher
            publicationController.GetPublicationDetails(
                (Model.Publication)dg_PublicationList.SelectedItem
                );

            Window window = new PublicationDetailsView(publicationController);
            window.Show();
        }
    }
}
