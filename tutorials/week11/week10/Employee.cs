using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week10
{
   public enum Gender { M, F, X };

    // Commenting for diff
    public class Employee
    {
        private string name;
        public string Name {
            get { return name; }
            set {
                if (value != null) name = value;
            }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Gender gender;
        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private List<TrainingSession> skills;
        public List<TrainingSession> Skills
        {
            get { return skills; }
            set { skills = value; }
        }

        // Another comment
        public override string ToString()
        {
            string to = name + " " + id + " " + gender + "\nSkills:\n";
            if (skills != null)
                foreach (TrainingSession skill in skills)
                    to += skill.ToString() + "\n";

            return to + "\n";
        }

        public int recentTraining()
        {
            DateTime now = DateTime.Now;
            // The kit206 database seems to come from around 2016, so
            // check dates relative to then.
            DateTime then = new DateTime(2016, now.Month, now.Day);

            // Select Employees with skills certified in the last 2 years
            DateTime recent = new DateTime(then.Year - 2, now.Month, now.Day);
            var filter = from s in skills
                         where s.Certified >= recent
                         select s;

            List<TrainingSession> recentTraining =
                new List<TrainingSession>(filter);

            // Return the number of skills certified in the last two years
            return recentTraining.Count;
        }
    }
}
