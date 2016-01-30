using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class RunManager : ITestManager<TestRun, TestRunProxy>
    {
        public List<TestRun> GetAll()
        {
            throw new NotImplementedException();
        }

        public TestRun GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TestRun Update(TestRunProxy proxy)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
