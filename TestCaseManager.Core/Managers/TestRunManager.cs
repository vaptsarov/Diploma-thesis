using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCaseManager.Core.Proxy.TestRun;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestRunManager : ITestManager<TestRun>
    {
        public List<TestRun> GetAll()
        {
            List<TestRun> testRun = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                testRun = context.TestRuns.ToList();
            }

            return testRun;
        }

        public TestRun Create(string name)
        {
            TestRun testRun = new TestRun();
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                testRun.Name = name;
                testRun.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";
                testRun.CreatedOn = DateTime.Now.ToUniversalTime();

                context.TestRuns.Add(testRun);
                context.SaveChanges();
            }

            return testRun;
        }

        public TestRun GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TestRun Update(TestRun proxy)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
