using System.Collections.Generic;
using System.Linq;
using TestCaseManager.Core.Proxy;
using TestCaseManager.DB;

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
                proxyObject.Areas = AreaModelToProxy(project.Areas.ToList());

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

                proxyAreaList.Add(proxyObject);
            }

            return proxyAreaList;
        }
    }
}
