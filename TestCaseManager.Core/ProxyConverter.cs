using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.DB;
using TestCaseManager.Utilities;

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
            proxyObject.StepDefinitionList = StepDefinitionModelToproxy(model.StepDefinitions);
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

        public static ObservableCollection<StepDefinitionProxy> StepDefinitionModelToproxy(ICollection<StepDefinition> stepDefinitions)
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
