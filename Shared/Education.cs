using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeWebsite.Shared
{
    public class Education
    {
        public string DegreePath { get; set; }  
        public string Institution { get; set; } 
        public string Description { get; set; }
        public string DateRange { get; set; }
        public Education(string degreePath, string institution, string description, string dateRange)
        {
            DegreePath = degreePath;
            Institution = institution;
            Description = description;
            DateRange = dateRange;
        }
    }
}
