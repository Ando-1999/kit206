using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RAP
{
    public enum EmploymentLevel { NULL, A, B, C, D, E, Student }

    public enum PublicationType
    {
        NULL,
        Conference,
        Journal,
        Other
    }

    public enum Campus
    {
        NULL,
        HOBART,
        LAUNCESTON,
        CRADLE_COAST
    }
    public enum ReportType
    {
        NULL,
        POOR,
        BELOW_EXPECTATIONS,
        MINIMUM_STANDARD,
        STAR_PERFORMANCE
    }

    public enum ResearcherType
    {
        NULL,
        Staff,
        Student
    }
    public partial class App : Application
    {
    }
}
