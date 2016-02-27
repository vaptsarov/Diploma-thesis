using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestCaseManager.Core.Managers.ProxyManagers;
using TestCaseManager.Core.Proxy;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class ProjectProxyManager : IProxyManager<ProjectProxy>
    {
        public ObservableCollection<ProjectProxy> GetAll()
        {
            ObservableCollection<ProjectProxy> configurations = new ObservableCollection<ProjectProxy>();
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                configurations = this.ProjectModelListToProxy(context);
            }

            return configurations;
        }

        private ObservableCollection<ProjectProxy> ProjectModelListToProxy(TestcaseManagerDB context)
        {
            ObservableCollection<ProjectProxy> proxyProjectList = new ObservableCollection<ProjectProxy>();
            foreach (Project project in context.Projects)
            {
                ProjectProxy proxyObject = ProxyConverter.ProjectModelToProxy(project);
                proxyObject.Areas = this.AreaModelListToProxy(project.Areas.ToList());

                proxyProjectList.Add(proxyObject);
            }

            return proxyProjectList;
        }

        private ObservableCollection<AreaProxy> AreaModelListToProxy(List<Area> areaList)
        {
            ObservableCollection<AreaProxy> proxyAreaList = new ObservableCollection<AreaProxy>();
            foreach (Area area in areaList)
            {
                AreaProxy proxyObject = ProxyConverter.AreaModelToProxy(area);
                proxyObject.TestCasesList = this.TestCaseModelListToProxy(area.TestCases.ToList());

                proxyAreaList.Add(proxyObject);
            }

            return proxyAreaList;
        }

        private ObservableCollection<TestCaseProxy> TestCaseModelListToProxy(List<TestCase> list)
        {
            ObservableCollection<TestCaseProxy> proxyTestCaseList = new ObservableCollection<TestCaseProxy>();
            foreach (TestCase item in list)
            {
                TestCaseProxy proxyObject = ProxyConverter.TestCaseModelToProxy(item);
                proxyTestCaseList.Add(proxyObject);
            }

            return proxyTestCaseList;
        }
    }
}
