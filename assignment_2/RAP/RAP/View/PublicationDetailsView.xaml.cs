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
    /// PublicationDetailsView.xaml 的交互逻辑
    /// </summary>
    public partial class PublicationDetailsView : Window
    {
        //Controller.PublicationController publicationController;
        public PublicationDetailsView(Controller.PublicationController publicationController)
        {
            this.DataContext = publicationController.PublicationDetails;

            InitializeComponent();
        }
    }
}
