using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestCaseManager.Core.Managers.ProxyManagers;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestRunProxyManager : IProxyManager<TestRunProxy>
    {
        public ObservableCollection<TestRunProxy> GetAll()
        {
            ObservableCollection<TestRunProxy> configurations = new ObservableCollection<TestRunProxy>();

            RunManager manager = new RunManager();
            configurations = this.RunModelListToProxy(manager.GetAll());

            return configurations;
        }

        private ObservableCollection<TestRunProxy> RunModelListToProxy(List<TestRun> list)
        {
            ObservableCollection<TestRunProxy> proxyList = new ObservableCollection<TestRunProxy>();
            foreach (TestRun item in list)
            {
                proxyList.Add(ProxyConverter.TestRunModelToProxy(item));
            }

            return proxyList;
        }
    }
}
