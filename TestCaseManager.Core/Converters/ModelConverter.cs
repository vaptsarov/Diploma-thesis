using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Converters
{
    public static class ModelConverter
    {
        public static Project ProjectProxyToModel(ProjectProxy proxy)
        {
            var model = new Project
            {
                Title = proxy.Title,
                ID = proxy.Id,
                CreatedBy = proxy.CreatedBy,
                UpdatedBy = proxy.UpdatedBy
            };

            return model;
        }

        public static Area AreaProxyToModel(AreaProxy proxy)
        {
            var area = new Area
            {
                ID = proxy.Id,
                Title = proxy.Title,
                CreatedBy = proxy.CreatedBy,
                UpdatedBy = proxy.UpdatedBy
            };

            return area;
        }

        public static TestCase TestCaseProxyToModel(TestCaseProxy proxy)
        {
            var testCase = new TestCase
            {
                ID = proxy.Id,
                Title = proxy.Title,
                Priority = proxy.Priority.ToString(),
                Severity = proxy.Severity.ToString(),
                IsAutomated = proxy.IsAutomated,
                CreatedBy = proxy.CreatedBy,
                UpdatedBy = proxy.UpdatedBy,
                AreaID = proxy.AreaId,
                StepDefinitions = StepDefinitionModelToProxy(proxy.StepDefinitionList)
            };

            return testCase;
        }

        public static ICollection<StepDefinition> StepDefinitionModelToProxy(
            ICollection<StepDefinitionProxy> stepDefinitions)
        {
            ICollection<StepDefinition> list = new Collection<StepDefinition>();
            foreach (var item in stepDefinitions)
            {
                var proxy = new StepDefinition
                {
                    Step = item.Step,
                    ExpectedResult = item.ExpectedResult,
                    ID = item.Id,
                    TestCaseID = item.TestCaseId
                };

                list.Add(proxy);
            }

            return list;
        }
    }
}