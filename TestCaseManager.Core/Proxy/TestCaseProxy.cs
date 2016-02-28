using System.Collections.ObjectModel;
using TestCaseManager.Core.Proxy.TestDefinition;

namespace TestCaseManager.Core.Proxy
{
    public class TestCaseProxy
    {
        public TestCaseProxy ()
        {
            this.StepDefinitionList = new ObservableCollection<StepDefinitionProxy>();
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
            AreaID = areaId;
            StepDefinitionList = stepDefinitionList;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Priority Priority { get; set; }
        public Severity Severity { get; set; }
        public bool IsAutomated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int AreaID { get; set; }
        public ObservableCollection<StepDefinitionProxy> StepDefinitionList { get; set; }
    }
}
