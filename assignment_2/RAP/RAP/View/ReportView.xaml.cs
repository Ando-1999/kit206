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
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : Window
    {
        Controller.ReportController reportController;
        public ReportView()
        {
            reportController =
                Application.Current.FindResource("reportController")
                as Controller.ReportController;

            this.DataContext = reportController;

            InitializeComponent();
        }

        public void copyEmails(object sender, RoutedEventArgs e)
        {
            reportController.copyEmails();
        }
    }
}
