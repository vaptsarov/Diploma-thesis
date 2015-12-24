using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Core.Models
{
    public class Area
    {
        public Area(string name)
        {
            Name = name;
            TestCasesList = new List<TestCase>();
        }

        public string Name { get; set; }

        public List<TestCase> TestCasesList { get; set; }
    }
}
