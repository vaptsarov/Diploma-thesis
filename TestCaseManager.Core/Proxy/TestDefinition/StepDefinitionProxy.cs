namespace TestCaseManager.Core.Proxy.TestDefinition
{
    public class StepDefinitionProxy
    {
        public int ID { get; set; }
        public string Step { get; set; }
        public string ExpectedResult { get; set; }
        public int TestCaseID { get; set; }
    }
}
