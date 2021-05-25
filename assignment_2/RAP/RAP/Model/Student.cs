using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Model
{
    class Student : Researcher
    {
        private string degree;
        public string Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        public int supervisorId;    // primary supervisor
        public int? SupervisorId
        {
            get { 
                return SupervisorId.HasValue
                    ? (int?)supervisorId
                    : null;
            }
            set {
                if (value != null)
                    supervisorId = (int)value;
            }
        }

        public Student()
        {
        }

        // Name of primary supervisor.
        // TODO: move to a controller?
        public string getSupervisorName()
        {
            Researcher supervisor =
                Database.ResearcherAdapter.fetchSupervisor(
                new Staff { Id = SupervisorId });

            return $"{supervisor.FirstName} {supervisor.LastName}";
        }

        public override string ToFullString()
        {
            return $"{Title} {FirstName} {LastName}\n" +
                $"{Degree}\n";
        }
    }
}
