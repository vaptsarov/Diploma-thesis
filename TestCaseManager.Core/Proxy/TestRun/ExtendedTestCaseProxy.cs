﻿namespace TestCaseManager.Core.Proxy.TestRun
{
    using TestStatus;

    public class ExtendedTestCaseProxy : TestCaseProxy
    {
        public ExtendedTestCaseProxy(TestCaseProxy proxy, Status status) :
            base(proxy.Id, proxy.Title, proxy.Priority, proxy.Severity, proxy.IsAutomated, proxy.CreatedBy,
                proxy.UpdatedBy, proxy.AreaId, proxy.StepDefinitionList)
        {
            Status = status;
        }

        public ExtendedTestCaseProxy()
        {
        }

        public Status Status { get; set; }
    }
}