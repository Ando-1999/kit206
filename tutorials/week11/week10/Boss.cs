using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace week10
{
    class Boss
    {
        private List<Employee> staff;
        private ObservableCollection<Employee> viewableStaff;
        public ObservableCollection<Employee> ViewableStaff
        {
            get { return viewableStaff; }
            set { viewableStaff = value; }
        }

        public Boss()
        {
            staff = Agency.LoadAll();
            LoadStaffTrainingSessions();
            var filter = from s in staff select s;
            ViewableStaff = new ObservableCollection<Employee>(filter);
        }

        public void LoadStaffTrainingSessions()
        {
            foreach (Employee s in staff)
                s.Skills = Agency.LoadTrainingSessions(s.Id);
        }

        // Manipulate list ObservableCollection with its own methods
        public void recentlyTrained_inPlace()
        {
            // Empty view list
            ViewableStaff.Clear();

            // Add staff who have receieved training recently to the list
            foreach (Employee s in staff)
                if (s.recentTraining() > 0)
                    ViewableStaff.Add(s);
        }

        public void recentlyTrained_replace()
        {
            // Select staff who have receieved training recently
            var filter = from s in staff
                         where s.recentTraining() > 0
                         select s;

            ObservableCollection<Employee> upToDate =
                new ObservableCollection<Employee>(filter);

            ViewableStaff = upToDate;
            /*
            A generic list might be able to successfully use a similar idiom
            without assigning ViewableStaff a new object:

                List<Employee> ViewableStaff;

                var filter = from s in staff
                             where s.recentTraining() > 0
                             select s;

                ViewableStaff.AddRange(filter);
            */
        }


        // Data in ListBox bound to this method
        public ObservableCollection<Employee> GetViewableList()
        {
            return ViewableStaff;
        }
    }
}
