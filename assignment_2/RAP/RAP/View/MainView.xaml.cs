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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace RAP.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        // researcherList controller
        Controller.ResearcherController researcherController;

        public MainView()
        {
            InitializeComponent();

            // Object bound to reasearcher ListBox
            researcherController = (Controller.ResearcherController)Application.Current.FindResource("researcherController");
        }

        private void GenerateReport(object sender, RoutedEventArgs e)
        {
            // TO-DO
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            string nameFilter = tb_Search.Text;

            EmploymentLevel? level = (EmploymentLevel?)cb_FilterByLevel.SelectedValue;
            EmploymentLevel levelFilter = level.HasValue ? level.Value : EmploymentLevel.NULL;

            if (researcherController.FilterName != nameFilter
                || researcherController.FilterLevel != levelFilter)
            {
                researcherController.FilterName = nameFilter;
                researcherController.FilterLevel = levelFilter;
                researcherController.filterList();
            }
        }

        private void viewResearcherDetails(object sender, SelectionChangedEventArgs e)
        {
            // Update ResearcherDetails with selected researcher
            researcherController.GetResearcherDetails(
                (Model.Researcher)researcherListBox.SelectedItem
                );

            Window researcherDetailsView = new ResearcherDetailsView();
            researcherDetailsView.Show();
        }
    }
}
