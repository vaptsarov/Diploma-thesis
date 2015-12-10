using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Views
{
    public class Employee
    {
        public Employee(string name)
        {
            Name = name;
            ManagedEmployees = new List<Employee>();
        }

        public string Name { get; set; }
        public List<Employee> ManagedEmployees { get; set; }
    }
}
