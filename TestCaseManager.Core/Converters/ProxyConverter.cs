using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.DB;
using TestCaseManager.Utilities;
using TestCaseManager.Core.Proxy.TestStatus;
using TestCaseManager.Core.Managers;

namespace TestCaseManager.Core
{
    public static class ProxyConverter
    {
        public static ProjectProxy ProjectModelToProxy(Project model)
        {
            ProjectProxy proxyObject = new ProjectProxy();
            proxyObject.Title = model.Title;
            proxyObject.ID = model.ID;
            proxyObject.CreatedBy = model.CreatedBy;
            proxyObject.UpdatedBy = model.UpdatedBy;

            return proxyObject;
        }
        public static AreaProxy AreaModelToProxy(Area model)
        {
            AreaProxy proxyObject = new AreaProxy();
            proxyObject.ID = model.ID;
            proxyObject.Title = model.Title;
            proxyObject.CreatedBy = model.CreatedBy;
            proxyObject.UpdatedBy = model.UpdatedBy;

            return proxyObject;
        }

        public static TestCaseProxy TestCaseModelToProxy(TestCase model)
        {
            TestCaseProxy proxyObject = new TestCaseProxy();
            proxyObject.Id = model.ID;
            proxyObject.Title = model.Title;
            proxyObject.Priority = EnumUtil.ParseEnum<Priority>(model.Priority);
            proxyObject.Severity = EnumUtil.ParseEnum<Severity>(model.Severity);
            proxyObject.IsAutomated = model.IsAutomated;
            proxyObject.CreatedBy = model.CreatedBy;
            proxyObject.UpdatedBy = model.UpdatedBy;
            proxyObject.AreaID = model.AreaID;

            foreach (var item in new TestManager().GetStepDefinitionsById(model.ID))
            {
                StepDefinitionProxy proxy = new StepDefinitionProxy();
                proxy.Step = item.Step;
                proxy.ExpectedResult = item.ExpectedResult;
                proxy.ID = item.ID;
                proxy.TestCaseID = item.TestCaseID;

                proxyObject.StepDefinitionList.Add(proxy);
            }

            return proxyObject;
        }

        public static TestRunProxy TestRunModelToProxy(TestRun run)
        {
            TestRunProxy runProxy = new TestRunProxy();
            runProxy.ID = run.ID;
            runProxy.Name = run.Name;
            runProxy.CreatedBy = run.CreatedBy;
            runProxy.CreatedOn = run.CreatedOn;

            IEnumerable<TestComposite> compositeModel = new TestRunManager().GetCompositeByRunId(runProxy.ID);
            foreach (TestComposite comp in compositeModel)
            {
                ExtendedTestCaseProxy extendedTestCaseProxy = new ExtendedTestCaseProxy();
                extendedTestCaseProxy.Status = EnumUtil.ParseEnum<Status>(comp.TestCaseStatus);

                TestCase testCase = new TestManager().GetById(comp.TestCaseID);
                extendedTestCaseProxy.Id = testCase.ID;
                extendedTestCaseProxy.Title = testCase.Title;
                extendedTestCaseProxy.Priority = EnumUtil.ParseEnum<Priority>(testCase.Priority);
                extendedTestCaseProxy.Severity = EnumUtil.ParseEnum<Severity>(testCase.Severity);
                extendedTestCaseProxy.IsAutomated = testCase.IsAutomated;
                extendedTestCaseProxy.CreatedBy = testCase.CreatedBy;
                extendedTestCaseProxy.UpdatedBy = testCase.UpdatedBy;
                extendedTestCaseProxy.AreaID = testCase.AreaID;

                foreach (var item in new TestManager().GetStepDefinitionsById(testCase.ID))
                {
                    StepDefinitionProxy proxy = new StepDefinitionProxy();
                    proxy.Step = item.Step;
                    proxy.ExpectedResult = item.ExpectedResult;
                    proxy.ID = item.ID;
                    proxy.TestCaseID = item.TestCaseID;

                    extendedTestCaseProxy.StepDefinitionList.Add(proxy);
                }

                runProxy.TestCasesList.Add(extendedTestCaseProxy);
            }

            return runProxy;
        }
    }
}
