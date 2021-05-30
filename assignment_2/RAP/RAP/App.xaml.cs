using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RAP
{
    public enum EmploymentLevel {
        [Display(Name = "Any")]
        NULL,
        [Display(Name = "A")]
        A,
        [Display(Name = "B")]
        B,
        [Display(Name = "C")]
        C,
        [Display(Name = "D")]
        D,
        [Display(Name = "E")]
        E,
        [Display(Name = "Student")]
        Student }

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
        [Display(Name = "None")]
        NULL,
        [Display(Name = "Poor")]
        POOR,
        [Display(Name = "Below Expectations")]
        BELOW_EXPECTATIONS,
        [Display(Name = "Minimum Standard")]
        MINIMUM_STANDARD,
        [Display(Name = "Star Performance")]
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

    /*
     * Converts Enum values to their Display Name.
     */
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type t = value.GetType();
            DisplayAttribute attr = null;
            System.Reflection.MemberInfo[] memberInfo = t.GetMember(value.ToString());

            try
            {
                attr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
            }
            catch (NullReferenceException e)
            {
                return "Error";
            }

            return attr.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*
            Dictionary<string, EmploymentLevel> convertBack =
                new Dictionary<string, EmploymentLevel>
                {
                    { "Any", EmploymentLevel.NULL },
                    { "A", EmploymentLevel.A },
                    { "B", EmploymentLevel.B },
                    { "C", EmploymentLevel.C },
                    { "D", EmploymentLevel.D },
                    { "E", EmploymentLevel.E },
                    { "Student", EmploymentLevel.Student },
                };
            return convertBack[(string)value];
            */
            throw new NotImplementedException();
        }
    }
}
