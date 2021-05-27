using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    public class Student : Researcher
    {
        private string degree;
        public string Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        public int? supervisorId;    // primary supervisor
        public int? SupervisorId { get; set; }

        public Student()
        {
        }


        public override string ToFullString()
        {
            return $"{Title} {FirstName} {LastName}\n" +
                $"{Degree}\n";
        }
    }
}
