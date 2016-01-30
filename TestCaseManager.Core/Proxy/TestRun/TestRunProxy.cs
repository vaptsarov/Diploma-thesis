﻿using System;
using System.Collections.ObjectModel;

namespace TestCaseManager.Core.Proxy.TestRun
{
    public class TestRunProxy
    {
        public TestRunProxy()
        {
            this.TestCasesList = new ObservableCollection<TestCaseProxy>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public ObservableCollection<TestCaseProxy> TestCasesList { get; set; }
    }
}
