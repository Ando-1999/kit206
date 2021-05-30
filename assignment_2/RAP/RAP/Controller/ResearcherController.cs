using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RAP.Controller
{
    public class ResearcherController
    {

        private string filterName;
        public string FilterName { get; set; }

        private EmploymentLevel filterLevel;
        public EmploymentLevel FilterLevel { get; set; }

        // Constant researcher list
        private List<Model.Researcher> researchers;
        public List<Model.Researcher> Researchers { get; set; }

        // Observable researcher list
        // Modify to filter list, etc.
        private ObservableCollection<Model.Researcher>
            researcherList;
        public ObservableCollection<Model.Researcher>
            ResearcherList { get; set; }

        private Model.Researcher researcherDetails;
        public Model.Researcher ResearcherDetails { get; set; }

        private string rType;
        public string RType { get; set;}

        // Expected number of publications for each employment level.
        readonly Dictionary<EmploymentLevel, double> expectedPublicationsByLevel =
            new Dictionary<EmploymentLevel, double>() {
                    { EmploymentLevel.A, 0.5},
                    { EmploymentLevel.B, 1},
                    { EmploymentLevel.C, 2},
                    { EmploymentLevel.D, 3.2},
                    { EmploymentLevel.E, 4},
            };

        public ResearcherController()
        {
            FilterName = "";
            FilterLevel = EmploymentLevel.NULL;
            // Load all researchers from database when controller is created.
            // Not sure if this is the best idea.
            Researchers = Database.ResearcherAdapter.fetchResearcherList();
            ResearcherList = new ObservableCollection<Model.Researcher>();
            filterList();
            //loadResearcherList();

            // No details initially loaded
            // TODO: Not sure of the best way to handle this, since
            // Researcher is abstract and static resources are bound
            // to an object, rather than a reference
            ResearcherDetails = null;
            RType = null;

            //How to handle the publications for a researcher?


        }


        /// <summary>
        /// GetResearcherList
        /// Returns the list of researchers for display
        /// </summary>
        public ObservableCollection<Model.Researcher> GetResearcherList()
        {
            return ResearcherList;
        }

        public Model.Researcher GetResearcherDetails(Model.Researcher researcher)
        {
            ResearcherDetails = Database.ResearcherAdapter
                                         .fetchResearcherDetails(researcher);

            RType = ResearcherDetails.GetType().ToString();

            return ResearcherDetails;
        }

        // Title of currently occupied position
        public string currentJobTitle()
        {
            if (ResearcherDetails.GetType() == typeof(Model.Student))
                return "Student";
            else
                return ResearcherDetails.Positions[0].jobTitle();
        }

        // Load cumulative publications for reasearcher.
        public List<Model.Publication> loadCumulativePublications()
        {
            return null;
        }

        /// Blake's Comment: Probably not necessary, as we use the adapter to fetch the research list
        /// Not removing for your opinion
        // Load basic details for all researchers.
        public void loadResearcherList()
        {

            // Instantiate ResearcherList
            if (ResearcherList == null)
            {
                ResearcherList =
                    new ObservableCollection<Model.Researcher>(Researchers);
            }
            // Reset ResearcherList to match Researcher
            else
            {
                ResearcherList.Clear();
                foreach (Model.Researcher r in Researchers)
                    ResearcherList.Add(r);
            }
        }

        /// Blake's Comment: You've done this in the Researcher model
        // Format list of researchers to be of the form "firstName, lastName
        // (title)".
        public List<string> formatList()
        {
            return null;
        }

        // Update filters (employment status, first name partial match,
        // last name partial match) based on user input.
        public void updateFilters()
        {
        }

        // Update researcher list to contain only those researchers with
        // details satisfying the current filters.
        // TODO: currently case sensitive
        public void filterList()
        {
            // Filter initially includes all researchers
            var filter = from r in Researchers
                         select r;

            // Filter by name
            if (!string.IsNullOrEmpty(FilterName))
                filter = from r in filter
                         where (r.FirstName.Contains(FilterName) || r.LastName.Contains(FilterName))
                         select r;

            // Filter by level
            if (FilterLevel != EmploymentLevel.NULL)
            {
                if (FilterLevel == EmploymentLevel.STUDENT)
                    filter = from r in filter
                             where r.GetType() == typeof(Model.Student)
                             select r;
                else
                    filter = from r in filter
                             where (r.GetType() == typeof(Model.Staff)
                                 && ((Model.Staff)r).Level == FilterLevel)
                             select r;
            }

            ResearcherList.Clear();

            foreach (Model.Researcher r in filter)
                ResearcherList.Add(r);
        }

        /// Blake's Comment: Same as loadResearcher
        /// <returns></returns>
        // Load list of students a staff member is or has ever supervised.
        public List<string> loadSupervisions()
        {
            return null;
        }

        /// Blake's Comment: Now, you call something similar in PublicationAdapter (totalPublications)
        /// So, do we want this in publications, or here?
        // Calculate cumulatie publications per year for a given researcher.
        public Dictionary<DateTime, int> calculateCumulativePublications()
        {
            return null;
        }

        /// Blake's Comment: Same as loadResearcher
        // Load details of researcher.
        public Model.Researcher loadResearcher()
        {
            return null;
        }

        /// Blake's Comment: Same as loadResearcher
        // Load name of researcher.
        public string loadResearcherName()
        {
            return null;
        }

        // Total time with institution in fractional years.
        public double? getTenure()
        {
            DateTime? start = null;
            DateTime? end = null;
            try
            {
                start = (DateTime?)ResearcherDetails.Positions[0].StartDate;
                end = (DateTime?)ResearcherDetails.Positions[0].EndDate;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
            catch (ArgumentOutOfRangeException e)
            {
                return null;
            }
            end = DateTime.Now;

            if (!start.HasValue)
                return null;

            if (!end.HasValue)
                end = DateTime.Now;

            TimeSpan tenure = end.Value - start.Value;

            return tenure.Days/365.0;
        }

        // Starting date of currently held position.
        public DateTime commencedCurrentPosition()
        {
            return (DateTime)ResearcherDetails.Positions[0].StartDate;
        }

        /*
         * Average number of publications authored in the last three whole
         * calendar years.
         * 
         * Staff only.
         */
        public double? threeYearAverage()
        {
            if (ResearcherDetails.GetType() == typeof(Model.Student))
                return null;

            double? numRecentPublications = Database.ReportAdapter.
                fetchNumRecentPublications(ResearcherDetails);

            return numRecentPublications / 3.0;
        }

        /* Three-year average divided by the expected number of publications
         * for employment level.
         * 
         * Staff only.
         */
        public double? getPerformance()
        {
            try
            {
                // Employment level of current position.
                EmploymentLevel level = ResearcherDetails.Positions[0].Level;

                // Only calculate perfomance for staff.
                if (level == EmploymentLevel.NULL || level == EmploymentLevel.STUDENT)
                    return null;

                return threeYearAverage() / expectedPublicationsByLevel[level];
            }
            catch (NullReferenceException e)
            {
                //MessageBox.Show(e.ToString());
                return null;
            }
            catch (ArgumentOutOfRangeException e)
            {
                //MessageBox.Show(e.ToString());
                return null;
            }
        }

        // Name of primary supervisor.
        public string getSupervisorName()
        {
            // Students only
            if (ResearcherDetails.GetType() == typeof(Model.Staff))
                return null;

            int? supervisorId = ((Model.Student)ResearcherDetails).SupervisorId;

            if (supervisorId == null)
                return null;

            Model.Staff supervisor =
                Database.ResearcherAdapter.fetchSupervisor(
                    new Model.Staff { Id = supervisorId }
                );

            return $"{supervisor.FirstName} {supervisor.LastName}";
        }


    }
}
