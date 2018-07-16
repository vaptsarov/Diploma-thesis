using System;

namespace TestCaseManager.Core.Proxy.TestDefinition
{
    [Serializable]
    public class StepDefinitionProxy
    {
        public int Id { get; set; }
        public string Step { get; set; }
        public string ExpectedResult { get; set; }
        public int TestCaseId { get; set; }
    }
}