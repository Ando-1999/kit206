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

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            controller.recentlyTrained_replace();
        }

        private void InPlace_Click(object sender, RoutedEventArgs e)
        {
            // Won't work after running Replace_Click because the list
            // property of the controller no longer points to the same object
            // as the data binding.
            //
            // Reassigning the object pointed to by ItemsSource of the
            // ListBox to the list property in the controller remedies the
            // situation.
            controller.ViewableStaff =
                (ObservableCollection<Employee>)list.ItemsSource;

            controller.recentlyTrained_inPlace();
        }
    }
}
