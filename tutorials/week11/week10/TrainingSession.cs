using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week10
{
    public enum Mode
    {
        Conference,
        Journal,
        Other
    };

    public class TrainingSession
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { if (value != null) title = value; }
        }
        private DateTime year;
        public DateTime Year
        {
            get { return year; }
            set { year = value; }
        }
        private DateTime certified;
        public DateTime Certified
        {
            get { return certified; }
            set { if (value != null) certified = value; }
        }
        private Mode mode;
        public Mode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        private int freshness;
        public int Freshness
        {
            get { return (int)(DateTime.Today - certified).TotalDays; }
        }

        public TrainingSession()
        { 
        }
        public TrainingSession(string t, DateTime y, DateTime c, Mode m)
        {
            title = t;
            year = y;
            certified = c;
            mode = m;
            freshness = Freshness;
        }

        public override string ToString()
        {
            string to = String.Format(
                "{0}\n\tcompeleted by {1} on {2}",
                title, mode, certified.ToShortDateString());
            return to;
        }

        
    }
}
