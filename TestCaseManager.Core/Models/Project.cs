using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Core.Models
{
    public class Project
    {
        public Project(string projectName)
        {
            ProjectName = projectName;
            Areas = new List<Area>();
        }

        public string ProjectName { get; set; }
        public List<Area> Areas { get; set; }
    }
}
