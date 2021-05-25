using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        // researcherList controller
        Controller.ResearcherListController researcherListController = new Controller.ResearcherListController();

        private const string RESEARCHER_LIST_KEY = "researcherList";


        private ObservableCollection<string> cb_GenerateReport_Items = new ObservableCollection<string>();
        private ObservableCollection<string> cb_FilterByLevel_Items = new ObservableCollection<string>();

        private ObservableCollection<Model.Researcher> researchers;

        public MainView()
        {
            InitializeComponent();

            // Object bound to reasearcher ListBox
            //researcherListController = (Controller.ResearcherListController)Application.Current.FindResource("researcher");

            //researchers = ()
            cb_GenerateReport_Items.Add("Poor");
            cb_GenerateReport_Items.Add("Below Expectations");
            cb_GenerateReport_Items.Add("Metting Minimum");
            cb_GenerateReport_Items.Add("Star Performers");
            cb_GenerateReport.ItemsSource = cb_GenerateReport_Items;  

            
            cb_FilterByLevel_Items.Add("Level A");
            cb_FilterByLevel_Items.Add("Level B");
            cb_FilterByLevel_Items.Add("Level C");
            cb_FilterByLevel_Items.Add("Level D");
            cb_FilterByLevel_Items.Add("Level E");
            cb_FilterByLevel_Items.Add("Student");
            cb_FilterByLevel.ItemsSource = cb_FilterByLevel_Items;

            researchers = researcherListController.loadResearcherList();
            dg_Researchers.ItemsSource = researchers;

            //ShowCommand = new DelegateCommand(Show, (obj) => true);
        }

        public ICommand ShowCommand 
        { get; set; }

        private void Show(object obj)
        {
            MessageBox.Show(obj.ToString());
        }

        private void ViewDetails(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            //MessageBox.Show("The text entered is: " + btn.Tag);

            Window window = new ResearcherDetailsView(btn.Tag.ToString());
            window.Show();
        }


        private void Filter(object sender, RoutedEventArgs e)
        {
            var query = from r in researchers
                        where r.FirstName.Contains(tb_Search.Text) or r.LastName.Contains(tb_Search.Text)
                        select r;
            dg_Researchers.ItemsSource = query.ToList();
        }
    }
}
