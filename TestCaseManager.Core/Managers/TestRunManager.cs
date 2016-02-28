using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            TestRun testRun = new TestRun();
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                testRun = context.TestRuns.Where(run => run.ID == id).FirstOrDefault();
            }

            return testRun;
        }

        public TestRun Update(TestRun proxy)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<TestComposite> GetCompositeByRunId(int id)
        {
            ICollection<TestComposite> collection = new Collection<TestComposite>();
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                var testRun = context.TestRuns.Where(run => run.ID == id).FirstOrDefault();

                if (testRun != null)
                    collection = testRun.TestComposites;
            }

            return collection;
        }
    }
}
