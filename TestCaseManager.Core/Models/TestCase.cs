using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Core.Models
{
    public class TestCase
    {
        public TestCase(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
