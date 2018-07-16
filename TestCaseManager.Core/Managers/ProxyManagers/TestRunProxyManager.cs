using System.Collections.ObjectModel;
using TestCaseManager.Core.Converters;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers.ProxyManagers
{
    public class TestRunProxyManager : IProxyManager<TestRunProxy>
    {
        public ObservableCollection<TestRunProxy> GetAll()
        {
            ObservableCollection<TestRunProxy> configurations;
            using (var context = new TestcaseManagerDB())
            {
                configurations = RunModelListToProxy(context);
            }

            return configurations;
        }

        private ObservableCollection<TestRunProxy> RunModelListToProxy(TestcaseManagerDB context)
        {
            var proxyList = new ObservableCollection<TestRunProxy>();
            foreach (var item in context.TestRuns) proxyList.Add(ProxyConverter.TestRunModelToProxy(item));

            return proxyList;
        }

        public TestRunProxy GetById(int runId)
        {
            var testRunModel = new TestRunManager().GetById(runId);
            var proxy = ProxyConverter.TestRunModelToProxy(testRunModel);

            return proxy;
        }
    }
}