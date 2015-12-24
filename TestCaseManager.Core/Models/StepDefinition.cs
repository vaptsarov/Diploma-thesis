using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Core.Models
{
    public sealed class StepDefinition
    {
        public string Step { get; set; }
        public string ExpectedResult { get; set; }
    }
}
