using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    class Student
    {
        public string degree;
        public Staff supervisor;    // primary supervisor

        public Student()
        {
        }

        // Name of primary supervisor.
        public string getSupervisorName()
        {
            return null;
        }
    }
}
