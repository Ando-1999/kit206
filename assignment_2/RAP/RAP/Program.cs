using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP
{
    enum EmploymentLevel { A, B, C, D, E }

    enum PublicationType
    { 
        Conference,
        Journal,
        Other
    }

    enum Campus
    {
        HOBART,
        LAUNCESTON,
        CRADLE_COAST

    }
    enum ReportType
    {
        POOR,
        BELOW_EXPECTATIONS,
        MINIMUM_STANDARD,
        STAR_PERFORMANCE
    }

    enum ResearcherType
    { 
        Researcher,
        Staff,
        Student
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Model.Researcher> rs =
                Database.ERDAdapter.fetchResearcherList();

            Model.Researcher r =
                Database.ERDAdapter.fetchResearcherDetails(rs[0]);

            List<Model.Publication> ps =
                Database.ERDAdapter.fetchPublicationsList(r);

            Model.Publication p =
                Database.ERDAdapter.fetchPublicationDetails(ps[0]);

            List<string> emails = Database.ERDAdapter.fetchResearcherEmails(rs);

            List<Model.Student> supervisions =
                Database.ERDAdapter.fetchSupervisions((Model.Staff)rs[1]);

            List<Model.Staff> staff =
                Database.ERDAdapter.fetchStaffList();
            foreach (Model.Staff s in staff)
                Console.WriteLine(s);
        }
    }
}
