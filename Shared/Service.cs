using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeWebsite.Shared
{
    public class Service
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Service(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
