﻿using System;
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

namespace RAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // researcherList controller
        Controller.ResearcherListController researcherController;

        public MainWindow()
        {
            InitializeComponent();

            // Object bound to reasearcher ListBox
            researcherController =
                (Controller.ResearcherListController)
                Application.Current.FindResource("researcher");
        }

        private void ListResearcher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                //After Task 4 done, this is not really needed
                MessageBox.Show("The selected item is: " + e.AddedItems[0]);
                //Part of task 4
                ResearcherDetailsPanel.DataContext = e.AddedItems[0];
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The text entered is: " + txtSearch.Text);
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        /*
        private void btnDeleteOne_Click(object sender, RoutedEventArgs e)
        {

            DetailsPanel.DataContext = new { Name = "Placeholder", PublicationCount = 69 };


            if (staff.VisibleWorkers.Count > 0)
            {
                Researcher theRemoved = staff.VisibleWorkers[0]; //this is just to keep the GUI tidy (after Task 4 implemented)
                staff.VisibleWorkers.RemoveAt(0); //the actual removal step
                //completing keeping the GUI tidy (something similar may be required in the assignment)
                if (DetailsPanel.DataContext == theRemoved)
                {
                    DetailsPanel.DataContext = null;
                }
            }
        }
        

        private void BtnTestWindow_Click(object sender, RoutedEventArgs e)
        {
            //Meme
            TestWindow win2 = new TestWindow();
            win2.Show();
        }
        */

    }
}