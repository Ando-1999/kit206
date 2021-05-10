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
using System.Collections.ObjectModel;

namespace week10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boss controller;
        public MainWindow()
        {
            InitializeComponent();

            controller = (Boss)Application.Current.FindResource("BossController");
        }

        // Badly behaved handler.  Doesn't cause the GUI to update 
        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            controller.recentlyTrained_replace();
        }

        // Well behaved handler.  Everything works as expected
        private void InPlace_Click(object sender, RoutedEventArgs e)
        {
            // Won't work after running Replace_Click because the ViewableStaff
            // property of the controller no longer points to the same object
            // as the data binding.
            //
            // Reassigning the object bound to the ItemsSource of the
            // ListBox to ViewableStaff in the controller remedies the
            // situation.
            controller.ViewableStaff =
                (ObservableCollection<Employee>)list.ItemsSource;

            controller.recentlyTrained_inPlace();
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            // Fix the bad behaviour of Replace_Click again
            controller.ViewableStaff =
                (ObservableCollection<Employee>)list.ItemsSource;

            controller.LoadStaff();
        }
    }
}
