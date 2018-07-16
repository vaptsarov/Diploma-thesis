using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestCaseManager.Core.AuthenticatePoint;
using TestCaseManager.Core.Proxy.TestStatus;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestRunManager : ITestManager<TestRun>
    {
        public List<TestRun> GetAll()
        {
            List<TestRun> testRun = null;
            using (var context = new TestcaseManagerDB())
            {
                testRun = context.TestRuns.ToList();
            }

            return testRun;
        }

        public TestRun GetById(int id)
        {
            TestRun testRun;
            using (var context = new TestcaseManagerDB())
            {
                testRun = context.TestRuns.FirstOrDefault(run => run.ID == id);
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

        public TestRun Create(string name)
        {
            var testRun = new TestRun();
            using (var context = new TestcaseManagerDB())
            {
                testRun.Name = name;
                testRun.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername;
                testRun.CreatedOn = DateTime.UtcNow;

                context.TestRuns.Add(testRun);
                context.SaveChanges();
            }

            return testRun;
        }

        public ICollection<TestComposite> GetCompositeByRunId(int id)
        {
            ICollection<TestComposite> collection = new Collection<TestComposite>();
            using (var context = new TestcaseManagerDB())
            {
                var testRun = context.TestRuns.FirstOrDefault(run => run.ID == id);

                if (testRun != null)
                    collection = testRun.TestComposites;
            }

            return collection;
        }

        public void RelateTestCaseToTestRun(int runId, ICollection<TestCase> testCases)
        {
            using (var context = new TestcaseManagerDB())
            {
                var runToUpdate = context.TestRuns.FirstOrDefault(r => r.ID == runId);
                ICollection<TestComposite> manyToManyRelationList = runToUpdate?.TestComposites.ToList();

                foreach (var testCase in testCases)
                    if (manyToManyRelationList != null && manyToManyRelationList.Any(cs => cs.TestCaseID == testCase.ID) == false)
                    {
                        var composite = new TestComposite
                        {
                            TestCaseID = testCase.ID,
                            TestRunID = runId,
                            TestCaseStatus = Status.NotExecuted.ToString()
                        };

                        runToUpdate.TestComposites.Add(composite);
                    }

                foreach (var composite in manyToManyRelationList)
                    if (testCases.Any(@case => @case.ID == composite.TestCaseID) == false)
                        runToUpdate?.TestComposites.Remove(composite);

                context.SaveChanges();
            }
        }

        public void UpdateTestCaseStatus(int runId, int testCaseId, Status status)
        {
            using (var context = new TestcaseManagerDB())
            {
                var testComposite = context.TestComposites.FirstOrDefault(comp => comp.TestRunID == runId && comp.TestCaseID == testCaseId);

                if (testComposite == null) return;

                testComposite.TestCaseStatus = status.ToString();
                context.SaveChanges();
            }
        }
    }
}