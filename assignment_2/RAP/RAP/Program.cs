using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP
{
    enum EmploymentLevel { A, B, C, D, E, Student }

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
        Staff,
        Student
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
            //testdb();
            //testResearcher();
            //testStaff();
            //testStudent();
            //testPublication();
        }

        static void testPublication()
        { 
            List<Model.Researcher> rs =
                Database.ResearcherAdapter.fetchResearcherList();
            Model.Researcher r = Database
                .ResearcherAdapter
                .fetchResearcherDetails(rs[1]);

            List<Model.Publication> ps = Database
                .PublicationAdapter
                .fetchPublicationsList(r);
            Model.Publication p = Database
                .PublicationAdapter
                .fetchPublicationDetails(ps[0]);

            Console.WriteLine(p.age());

        }
        static void testStudent()
        { 
            List<Model.Researcher> rs =
                Database.ResearcherAdapter.fetchResearcherList();

            Model.Student s =
                (Model.Student)Database
                    .ResearcherAdapter
                    .fetchResearcherDetails(rs[6]);

            Console.WriteLine(s.getSupervisorName());
        }
        static void testStaff()
        { 
            List<Model.Researcher> rs =
                Database.ResearcherAdapter.fetchResearcherList();

            Model.Staff s =
                (Model.Staff)Database
                .ResearcherAdapter
                .fetchResearcherDetails(rs[1]);

            Console.WriteLine(s.threeYearAverage());
            Console.WriteLine(s.Performance);
            Console.WriteLine(s.supervisions());
        }

        static void testResearcher()
        {
            List<Model.Researcher> rs =
                Database.ResearcherAdapter.fetchResearcherList();

            Model.Researcher r = rs[0];
            Database.ResearcherAdapter.fetchResearcherDetails(r);

            // TODO: Requires researcher details
            // Is that ok?  I think so.
            Console.WriteLine(r.currentJobTitle());
            Console.WriteLine(r.commencedCurrentPosition()
                .ToShortDateString());
            Console.WriteLine(r.tenure());
            Console.WriteLine(r.numberOfPublications());
            foreach (Model.Publication p in r.getPublications())
                Console.WriteLine(p.ToString());
            foreach (Model.Position p in r.getPositions())
                Console.WriteLine(p.ToString());
        }

        static void testdb()
        {
            List<Model.Researcher> rs =
                Database.ResearcherAdapter.fetchResearcherList();
            Console.WriteLine("fetchResearcherList():");
            foreach (Model.Researcher researcher in rs)
                Console.WriteLine(researcher);
            Console.WriteLine("");

            Model.Researcher r =
                Database.ResearcherAdapter.fetchResearcherDetails(rs[2]);
            Console.WriteLine("fetchResearcherDetails():");
            Console.WriteLine(r.ToFullString());

            List<Model.Publication> ps =
                Database.PublicationAdapter.fetchPublicationsList(r);
            Console.WriteLine("fetchPublicationsList():");
            foreach (Model.Publication publication in ps)
                Console.WriteLine(publication);
            Console.WriteLine("");

            Model.Publication p =
                Database.PublicationAdapter.fetchPublicationDetails(ps[0]);
            Console.WriteLine("fetchPublicationDetails():");
            Console.WriteLine(p.ToFullString());
            Console.WriteLine("");

            List<string> emails =
                Database.ReportAdapter.fetchResearcherEmails(rs);
            Console.WriteLine("fetchResearcherEmails():");
            foreach (string email in emails)
                Console.WriteLine(email);
            Console.WriteLine("");

            List<Model.Student> supervisions =
                Database.ResearcherAdapter.fetchSupervisions(
                    (Model.Staff)rs[1]);
            Console.WriteLine("fetchSupervisions():");
            foreach (Model.Student supervision in supervisions)
                Console.WriteLine(supervision);
            Console.WriteLine("");

            int numSupervisions =
                Database.ResearcherAdapter.fetchNumSupervisions(
                    (Model.Staff)rs[1]);
            Console.WriteLine("fetchNumSupervisions():");
            Console.WriteLine(numSupervisions);
            Console.WriteLine("");

            List<Model.Staff> staffList =
                Database.ReportAdapter.fetchStaffList();
            Console.WriteLine("fetchStaffList():");
            foreach (Model.Staff staff in staffList)
                Console.WriteLine(staff);
            Console.WriteLine("");

            Model.Researcher res =
                Database.ResearcherAdapter.fetchResearcherDetails(rs[6]);
            List<Model.Position> positions =
                Database.ResearcherAdapter.fetchPositions(res);
            Console.WriteLine("fetchPositions():");
            foreach (Model.Position position in res.Positions)
                Console.WriteLine(position);
            Console.WriteLine("");


            int numRecentPubs =
                Database.ReportAdapter.fetchNumRecentPublications(res);
            Console.WriteLine("fetchNumRecentPublications():");
            Console.WriteLine(numRecentPubs );
            Console.WriteLine("");

            int numPubs =
                Database.PublicationAdapter.totalPublications(res);
            Console.WriteLine("totalPublications():");
            Console.WriteLine(res.Id);
            Console.WriteLine(numPubs );
            Console.WriteLine("");
        }
    }
}
