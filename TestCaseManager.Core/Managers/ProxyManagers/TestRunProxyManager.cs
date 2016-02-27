using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestCaseManager.Core.Managers.ProxyManagers;
using TestCaseManager.Core.Proxy.TestRun;
using System.Linq;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestRunProxyManager : IProxyManager<TestRunProxy>
    {
        public ObservableCollection<TestRunProxy> GetAll()
        {
            ObservableCollection<TestRunProxy> configurations = new ObservableCollection<TestRunProxy>();
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                configurations = this.RunModelListToProxy(context);
            }

            return configurations;
        }

        private ObservableCollection<TestRunProxy> RunModelListToProxy(TestcaseManagerDB context)
        {
            ObservableCollection<TestRunProxy> proxyList = new ObservableCollection<TestRunProxy>();
            foreach (TestRun item in context.TestRuns)
            {
                proxyList.Add(ProxyConverter.TestRunModelToProxy(item));
            }

            return proxyList;
        }

        public TestRunProxy GetById(int runId)
        {
            TestRun testRunModel = new TestRunManager().GetById(runId);
            TestRunProxy proxy = ProxyConverter.TestRunModelToProxy(testRunModel);

            return proxy;
        }
    }
}
