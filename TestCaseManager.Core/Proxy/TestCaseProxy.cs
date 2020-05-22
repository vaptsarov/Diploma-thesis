namespace TestCaseManager.Core.Proxy
{
    using System.Collections.ObjectModel;
    using TestDefinition;

    public class TestCaseProxy
    {
        public TestCaseProxy()
        {
            StepDefinitionList = new ObservableCollection<StepDefinitionProxy>();
        }

        public TestCaseProxy(
            int id,
            string title,
            Priority priority,
            Severity severity,
            bool isAutomated,
            string createdBy,
            string updatedBy,
            int areaId,
            ObservableCollection<StepDefinitionProxy> stepDefinitionList)
        {
            Id = id;
            Title = title;
            Priority = priority;
            Severity = severity;
            IsAutomated = isAutomated;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            AreaId = areaId;
            StepDefinitionList = stepDefinitionList;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public Severity Severity { get; set; }
        public bool IsAutomated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int AreaId { get; set; }
        public ObservableCollection<StepDefinitionProxy> StepDefinitionList { get; set; }
    }
}