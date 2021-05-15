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
        CONFERENCE,
        JOURNAL,
        OTHER
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

    class Program
    {
        static void Main(string[] args)
        {
            //Database.ERDAdapter.fetchResearcherEmails(new List<Model.Researcher>());


            //foreach (var r in Model.Researcher.All()
            //foreach (var r in Model.Publication.All())
            foreach (var r in Model.ResearcherPublication.All())
            {
                Console.WriteLine(r);
            }

            // get all the publications of one researcher
            var query = from rp in Model.ResearcherPublication.All()
                        join p in Model.Publication.All() on rp.Doi equals p.Doi
                        where rp.ResearcherId == 123460
                        select p;
            foreach (var x in query)
            {
                Console.WriteLine(x);
            }
        }
    }
}
