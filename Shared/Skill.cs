using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeWebsite.Shared
{
    public class Skill
    {
        public string Name { get; set; }
        public int Years { get; set; }
        public int Percentage { get; set; }
        public Skill(string name, int years, int percentage)
        {
            Name = name;
            Years = years;
            Percentage = percentage;
        }
    }
}
