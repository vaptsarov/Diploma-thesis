using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestCaseManager.Core.Proxy.TestStatus;
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
                testRun.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername;
                testRun.CreatedOn = DateTime.UtcNow;

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

        public void RelateTestCaseToTestRun(int runId, ICollection<TestCase> testCases)
        {
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                TestRun runToUpdate = context.TestRuns.Where(r => r.ID == runId).FirstOrDefault();
                ICollection<TestComposite> manyToManyRelationList = runToUpdate.TestComposites.ToList();

                foreach (TestCase testCase in testCases)
                {
                    if(manyToManyRelationList.Any(cs => cs.TestCaseID == testCase.ID) == false)
                    {
                        TestComposite composite = new TestComposite();
                        composite.TestCaseID = testCase.ID;
                        composite.TestRunID = runId;
                        composite.TestCaseStatus = Status.NotExecuted.ToString();

                        runToUpdate.TestComposites.Add(composite);               
                    }
                }

                foreach (TestComposite composite in manyToManyRelationList)
                {
                    if(testCases.Any(@case=>@case.ID == composite.TestCaseID) == false)
                    {
                        runToUpdate.TestComposites.Remove(composite);
                    }
                }

                context.SaveChanges();
            }
        }

        public void UpdateTestCaseStatus(int runId, int testCaseId, Status status)
        {
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                TestComposite testComposite = context.TestComposites.Where(comp => comp.TestRunID == runId && comp.TestCaseID == testCaseId).FirstOrDefault();

                if(testComposite != null)
                {
                    testComposite.TestCaseStatus = status.ToString();
                    context.SaveChanges();
                }
            }
        }
    }
}
