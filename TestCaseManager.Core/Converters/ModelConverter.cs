using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.DB;
using TestCaseManager.Utilities;
using TestCaseManager.Core.Proxy.TestStatus;

namespace TestCaseManager.Core
{
    public static class ModelConverter
    {
        public static Project ProjectProxyToModel(ProjectProxy proxy)
        {
            Project model = new Project();
            model.Title = proxy.Title;
            model.ID = proxy.ID;
            model.CreatedBy = proxy.CreatedBy;
            model.UpdatedBy = proxy.UpdatedBy;

            return model;
        }

        public static Area AreaProxyToModel(AreaProxy proxy)
        {
            Area area = new Area();
            area.ID = proxy.ID;
            area.Title = proxy.Title;
            area.CreatedBy = proxy.CreatedBy;
            area.UpdatedBy = proxy.UpdatedBy;

            return area;
        }

        public static TestCase TestCaseProxyToModel(TestCaseProxy proxy)
        {
            TestCase testCase = new TestCase();
            testCase.ID = proxy.Id;
            testCase.Title = proxy.Title;
            testCase.Priority = proxy.Priority.ToString();
            testCase.Severity = proxy.Severity.ToString();
            testCase.IsAutomated = proxy.IsAutomated;
            testCase.CreatedBy = proxy.CreatedBy;
            testCase.UpdatedBy = proxy.UpdatedBy;
            testCase.AreaID = proxy.AreaID;
            testCase.StepDefinitions = StepDefinitionModelToProxy(proxy.StepDefinitionList);

            return testCase;
        }

        public static ICollection<StepDefinition> StepDefinitionModelToProxy(ICollection<StepDefinitionProxy> stepDefinitions)
        {
            ICollection<StepDefinition> list = new Collection<StepDefinition>();
            foreach (var item in stepDefinitions)
            {
                StepDefinition proxy = new StepDefinition();
                proxy.Step = item.Step;
                proxy.ExpectedResult = item.ExpectedResult;
                proxy.ID = item.ID;
                proxy.TestCaseID = item.TestCaseID;

                list.Add(proxy);
            }

            return list;
        }
    }
}
