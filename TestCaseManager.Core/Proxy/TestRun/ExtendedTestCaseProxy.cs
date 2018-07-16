using TestCaseManager.Core.Proxy.TestStatus;

namespace TestCaseManager.Core.Proxy.TestRun
{
    public class ExtendedTestCaseProxy : TestCaseProxy
    {
        public ExtendedTestCaseProxy()
        {
        }

        public ExtendedTestCaseProxy(TestCaseProxy proxy, Status status) :
            base(proxy.Id, proxy.Title, proxy.Priority, proxy.Severity, proxy.IsAutomated, proxy.CreatedBy,
                proxy.UpdatedBy, proxy.AreaId, proxy.StepDefinitionList)
        {
            Status = status;
        }

        public Status Status { get; set; }
    }
}