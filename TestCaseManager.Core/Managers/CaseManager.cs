using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.DB;
using TestCaseManager.Utilities;

namespace TestCaseManager.Core.Managers
{
    public class CaseManager
    {
        public List<ProjectProxy> GetAll()
        {
            List<ProjectProxy> configurations = new List<ProjectProxy>();

            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                configurations = this.ProjectModelToProxy(context);
            }

            return configurations;
        }

        private List<ProjectProxy> ProjectModelToProxy(TestcaseManagerDB context)
        {
            List<ProjectProxy> proxyProjectList = new List<ProjectProxy>();
            foreach (Project project in context.Projects)
            {
                ProjectProxy proxyObject = new ProjectProxy(project.Title);
                proxyObject.ID = project.ID;
                proxyObject.CreatedBy = project.CreatedBy;
                proxyObject.UpdatedBy = project.UpdatedBy;
                proxyObject.Areas = this.AreaModelToProxy(project.Areas.ToList());

                proxyProjectList.Add(proxyObject);
            }

            return proxyProjectList;
        }

        private List<AreaProxy> AreaModelToProxy(List<Area> areaList)
        {
            List<AreaProxy> proxyAreaList = new List<AreaProxy>();
            foreach (Area area in areaList)
            {
                AreaProxy proxyObject = new AreaProxy();
                proxyObject.ID = area.ID;
                proxyObject.Title = area.Title;
                proxyObject.CreatedBy = area.CreatedBy;
                proxyObject.UpdatedBy = area.UpdatedBy;
                proxyObject.TestCasesList = this.TestCaseModelToProxy(area.TestCases.ToList());

                proxyAreaList.Add(proxyObject);
            }

            return proxyAreaList;
        }

        private List<TestCaseProxy> TestCaseModelToProxy(List<TestCas> list)
        {
            List<TestCaseProxy> proxyTestCaseList = new List<TestCaseProxy>();
            foreach (var item in list)
            {
                TestCaseProxy proxyObject = new TestCaseProxy();
                proxyObject.Id = item.ID;
                proxyObject.Title = item.Title;
                proxyObject.Priority = EnumUtil.ParseEnum<Priority>(item.Priority);
                proxyObject.Severity = EnumUtil.ParseEnum<Severity>(item.Severity);
                proxyObject.IsAutomated = item.IsAutomated;
                proxyObject.CreatedBy = item.CreatedBy;
                proxyObject.UpdatedBy = item.UpdatedBy;

                proxyTestCaseList.Add(proxyObject);
            }

            return proxyTestCaseList;
        }
    }
}
