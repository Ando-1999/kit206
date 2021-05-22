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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RAP_WPF.View
{
    /// <summary>
    /// Interaction logic for LevelFilterUC.xaml
    /// </summary>
    public partial class LevelFilterUC : UserControl
    {
        public LevelFilterUC()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //The if prevents taking any action when the *first* item is selected, which is done on start up.
            if (e.RemovedItems.Count > 0)
            {
                MessageBox.Show("Dropdown list used to select: " + e.AddedItems[0]);
                //listResearcher.FilterByLevel(l);
            }
            //Consider assigning genders to the Employee objects populated from the HRIS database (which do not have a gender),
            //then use code similar to that in MainWindow to gain access to the Boss object and request that it filter
            //the list by the selected Gender.
        }
    }
}
