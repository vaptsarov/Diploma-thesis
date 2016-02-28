using TestCaseManager.Core.Proxy.TestStatus;

namespace TestCaseManager.Core.Proxy.TestRun
{
    public class ExtendedTestCaseProxy : TestCaseProxy
    { 
        public Status Status { get; set; }

        public ExtendedTestCaseProxy() { }

        public ExtendedTestCaseProxy(TestCaseProxy proxy, Status status) :
            base(proxy.Id, proxy.Title, proxy.Priority, proxy.Severity, proxy.IsAutomated, proxy.CreatedBy, proxy.UpdatedBy, proxy.AreaID, proxy.StepDefinitionList)
        {
            Status = status;
        }
    }
}
