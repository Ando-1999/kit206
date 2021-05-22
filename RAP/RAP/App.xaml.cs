using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
    public partial class App : Application
    {
    }
}
