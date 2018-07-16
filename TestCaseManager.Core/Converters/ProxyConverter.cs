using System.Collections.Generic;
using TestCaseManager.Core.Managers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.Core.Proxy.TestStatus;
using TestCaseManager.DB;
using TestCaseManager.Utilities.StringUtility;

namespace TestCaseManager.Core.Converters
{
    public static class ProxyConverter
    {
        public static ProjectProxy ProjectModelToProxy(Project model)
        {
            var proxyObject = new ProjectProxy
            {
                Title = model.Title,
                Id = model.ID,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            return proxyObject;
        }

        public static AreaProxy AreaModelToProxy(Area model)
        {
            var proxyObject = new AreaProxy
            {
                Id = model.ID,
                Title = model.Title,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy
            };

            return proxyObject;
        }

        public static TestCaseProxy TestCaseModelToProxy(TestCase model)
        {
            var proxyObject = new TestCaseProxy
            {
                Id = model.ID,
                Title = model.Title,
                Priority = EnumUtil.ParseEnum<Priority>(model.Priority),
                Severity = EnumUtil.ParseEnum<Severity>(model.Severity),
                IsAutomated = model.IsAutomated,
                CreatedBy = model.CreatedBy,
                UpdatedBy = model.UpdatedBy,
                AreaId = model.AreaID
            };

            foreach (var item in new TestManager().GetStepDefinitionsById(model.ID))
            {
                var proxy = new StepDefinitionProxy
                {
                    Step = item.Step,
                    ExpectedResult = item.ExpectedResult,
                    Id = item.ID,
                    TestCaseId = item.TestCaseID
                };

                proxyObject.StepDefinitionList.Add(proxy);
            }

            return proxyObject;
        }

        public static TestRunProxy TestRunModelToProxy(TestRun run)
        {
            var runProxy = new TestRunProxy
            {
                Id = run.ID,
                Name = run.Name,
                CreatedBy = run.CreatedBy,
                CreatedOn = run.CreatedOn
            };

            IEnumerable<TestComposite> compositeModel = new TestRunManager().GetCompositeByRunId(runProxy.Id);
            foreach (var comp in compositeModel)
            {
                var extendedTestCaseProxy = new ExtendedTestCaseProxy
                {
                    Status = EnumUtil.ParseEnum<Status>(comp.TestCaseStatus)
                };

                var testCase = new TestManager().GetById(comp.TestCaseID);
                extendedTestCaseProxy.Id = testCase.ID;
                extendedTestCaseProxy.Title = testCase.Title;
                extendedTestCaseProxy.Priority = EnumUtil.ParseEnum<Priority>(testCase.Priority);
                extendedTestCaseProxy.Severity = EnumUtil.ParseEnum<Severity>(testCase.Severity);
                extendedTestCaseProxy.IsAutomated = testCase.IsAutomated;
                extendedTestCaseProxy.CreatedBy = testCase.CreatedBy;
                extendedTestCaseProxy.UpdatedBy = testCase.UpdatedBy;
                extendedTestCaseProxy.AreaId = testCase.AreaID;

                foreach (var item in new TestManager().GetStepDefinitionsById(testCase.ID))
                {
                    var proxy = new StepDefinitionProxy
                    {
                        Step = item.Step,
                        ExpectedResult = item.ExpectedResult,
                        Id = item.ID,
                        TestCaseId = item.TestCaseID
                    };

                    extendedTestCaseProxy.StepDefinitionList.Add(proxy);
                }

                runProxy.TestCasesList.Add(extendedTestCaseProxy);
            }

            return runProxy;
        }
    }
}