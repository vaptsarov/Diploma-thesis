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
    public static class ProxyConverter
    {
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

        public static ProjectProxy ProjectModelToProxy(Project model)
        {
            ProjectProxy proxyObject = new ProjectProxy();
            proxyObject.Title = model.Title;
            proxyObject.ID = model.ID;
            proxyObject.CreatedBy = model.CreatedBy;
            proxyObject.UpdatedBy = model.UpdatedBy;

            return proxyObject;
        }

        public static TestRunProxy TestRunModelToProxy(TestRun run)
        {
            TestRunProxy runProxy = new TestRunProxy();
            runProxy.ID = run.ID;
            runProxy.Name = run.Name;
            runProxy.CreatedBy = run.CreatedBy;
            runProxy.CreatedOn = run.CreatedOn;

            IEnumerable<TestComposite> compositeModel = run.TestComposites.Where(comp => comp.TestRunID == run.ID);
            foreach (TestComposite comp in compositeModel)
            {
                ExtendedTestCaseProxy extendedTestCaseProxy = new ExtendedTestCaseProxy();
                extendedTestCaseProxy.Status = EnumUtil.ParseEnum<Status>(comp.TestCaseStatus);

                TestCase testCase = comp.TestCas;
                extendedTestCaseProxy.Id = testCase.ID;
                extendedTestCaseProxy.Title = testCase.Title;
                extendedTestCaseProxy.Priority = EnumUtil.ParseEnum<Priority>(testCase.Priority);
                extendedTestCaseProxy.Severity = EnumUtil.ParseEnum<Severity>(testCase.Severity);
                extendedTestCaseProxy.IsAutomated = testCase.IsAutomated;
                extendedTestCaseProxy.CreatedBy = testCase.CreatedBy;
                extendedTestCaseProxy.UpdatedBy = testCase.UpdatedBy;
                extendedTestCaseProxy.AreaID = testCase.AreaID;

                runProxy.TestCasesList.Add(extendedTestCaseProxy);
            }

            return runProxy;
        }

        public static ObservableCollection<StepDefinitionProxy> StepDefinitionModelToProxy(ICollection<StepDefinition> stepDefinitions)
        {
            ObservableCollection<StepDefinitionProxy> list = new ObservableCollection<StepDefinitionProxy>();
            foreach (var item in stepDefinitions)
            {
                StepDefinitionProxy proxy = new StepDefinitionProxy();
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
