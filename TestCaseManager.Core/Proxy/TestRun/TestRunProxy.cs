namespace TestCaseManager.Core.Proxy.TestRun
{
    using System;
    using System.Collections.ObjectModel;

    public class TestRunProxy
    {
        public TestRunProxy()
        {
            TestCasesList = new ObservableCollection<ExtendedTestCaseProxy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public ObservableCollection<ExtendedTestCaseProxy> TestCasesList { get; set; }
    }
}