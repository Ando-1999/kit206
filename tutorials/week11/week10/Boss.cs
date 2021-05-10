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
        public List<Employee> Staff
        {
            get { return staff; }
            set { staff = value; }
        }

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
            LoadStaff();
        }

        public void LoadStaff()
        {
            // Initialize ViewableStaff.
            if (ViewableStaff == null)
            {
                ViewableStaff = new ObservableCollection<Employee>(
                                    from s in staff select s
                                );
            }
            // Reset ViewableStaff to contain the full staff list.
            else
            {
                viewableStaff.Clear();

                foreach (Employee s in staff)
                    ViewableStaff.Add(s);
            }
        }

        public void LoadStaffTrainingSessions()
        {
            foreach (Employee s in staff)
                s.Skills = Agency.LoadTrainingSessions(s.Id);
        }

        // Manipulate ViewableStaff with its own methods.
        // Works as expected.
        public void recentlyTrained_inPlace()
        {
            // Empty view list
            ViewableStaff.Clear();

            // Add staff to the list if they have receieved training recently
            foreach (Employee s in staff)
                if (s.recentTraining() > 0)
                    ViewableStaff.Add(s);
        }

        // Change ViewableStaff by assigning it a new object.
        // Doesn't work, as expected.
        public void recentlyTrained_replace()
        {
            // Select staff who have receieved training recently
            var filter = from s in staff
                         where s.recentTraining() > 0
                         select s;

            ViewableStaff = new ObservableCollection<Employee>(filter);
            /*
            A generic list might be able to successfully use a similar idiom
            without assigning ViewableStaff a new object, e.g.

                List<Employee> ViewableStaff;

                var filter = from s in staff
                             where s.recentTraining() > 0
                             select s;

                ViewableStaff.AddRange(filter);
            */
        }


        // Data in ListBox bound to this method.
        public ObservableCollection<Employee> GetViewableList()
        {
            return ViewableStaff;
        }
    }
}
