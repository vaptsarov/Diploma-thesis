namespace TestCaseManager.Core.Managers.ProxyManagers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Converters;
    using DB;
    using Proxy;

    public class ProjectProxyManager : IProxyManager<ProjectProxy>
    {
        public ObservableCollection<ProjectProxy> GetAll()
        {
            ObservableCollection<ProjectProxy> configurations;
            using (var context = new TestcaseManagerDB())
            {
                configurations = ProjectModelListToProxy(context);
            }

            return configurations;
        }

        private ObservableCollection<ProjectProxy> ProjectModelListToProxy(TestcaseManagerDB context)
        {
            var proxyProjectList = new ObservableCollection<ProjectProxy>();
            foreach (var project in context.Projects)
            {
                var proxyObject = ProxyConverter.ProjectModelToProxy(project);
                proxyObject.Areas = AreaModelListToProxy(project.Areas.ToList());

                proxyProjectList.Add(proxyObject);
            }

            return proxyProjectList;
        }

        private ObservableCollection<AreaProxy> AreaModelListToProxy(List<Area> areaList)
        {
            var proxyAreaList = new ObservableCollection<AreaProxy>();
            foreach (var area in areaList)
            {
                var proxyObject = ProxyConverter.AreaModelToProxy(area);
                proxyObject.TestCasesList = TestCaseModelListToProxy(area.TestCases.ToList());

                proxyAreaList.Add(proxyObject);
            }

            return proxyAreaList;
        }

        private ObservableCollection<TestCaseProxy> TestCaseModelListToProxy(List<TestCase> list)
        {
            var proxyTestCaseList = new ObservableCollection<TestCaseProxy>();
            foreach (var item in list)
            {
                var proxyObject = ProxyConverter.TestCaseModelToProxy(item);
                proxyTestCaseList.Add(proxyObject);
            }

            return proxyTestCaseList;
        }
    }
}