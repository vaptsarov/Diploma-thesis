using System;

namespace TestCaseManager.Core.Proxy.TestDefinition
{
    [Serializable]
    public class StepDefinitionProxy
    {
        public int ID { get; set; }
        public string Step { get; set; }
        public string ExpectedResult { get; set; }
        public int TestCaseID { get; set; }
    }
}
