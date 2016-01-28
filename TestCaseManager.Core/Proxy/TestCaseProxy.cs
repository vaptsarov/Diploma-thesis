﻿using System.Collections.ObjectModel;
using TestCaseManager.Core.Proxy.TestDefinition;

namespace TestCaseManager.Core.Proxy
{
    public class TestCaseProxy
    {
        public TestCaseProxy()
        {
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public Severity Severity { get; set; }
        public bool IsAutomated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public ObservableCollection<StepDefinitionProxy> StepDefinitionList { get; set; }
    }
}
