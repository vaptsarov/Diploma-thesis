using TestCaseManager.Core.Models.TestDefinition;

namespace TestCaseManager.Core.Models
{
    public class TestCase
    {
        public TestCase(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public Severity Severity { get; set; }
        public bool IsAutomated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
