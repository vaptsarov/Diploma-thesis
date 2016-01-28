using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestManager : ITestManager<TestCase>
    {
        public TestCase Create(int areaId, TestCaseProxy testCase)
        {
            TestCase @case = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                @case = new TestCase();
                @case.Title = testCase.Title;
                @case.AreaID = areaId;
                @case.Severity = testCase.Severity.ToString();
                @case.Priority = testCase.Priority.ToString();
                @case.IsAutomated = testCase.IsAutomated;

                @case.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";

                context.TestCases.Add(@case);
                context.SaveChanges();

                if (testCase.StepDefinitionList != null)
                {
                    foreach (StepDefinitionProxy item in testCase.StepDefinitionList)
                    {
                        if (item.Step != null)
                        {
                            StepDefinition definition = new StepDefinition();
                            definition.Step = item.Step;
                            definition.ExpectedResult = item.ExpectedResult;
                            definition.TestCaseID = @case.ID;
                            @case.StepDefinitions.Add(definition);
                        }
                    }

                    context.SaveChanges();
                }
            }

            return @case;
        }

        public List<TestCase> GetAll()
        {
            List<TestCase> testCasesList = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                testCasesList = context.TestCases.ToList();
            }

            return testCasesList;
        }

        public TestCase GetById(int id)
        {
            TestCase testCase = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                testCase = context.TestCases.Where(tc => tc.ID == id).FirstOrDefault();
            }

            //TODO: Throw custom exception for null project.
            if (testCase == null)
                throw new NullReferenceException();

            return testCase;
        }

        public void Update(int id, string title)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                TestCase testCase = context.TestCases.Where(tc => tc.ID == id).FirstOrDefault();

                if (testCase == null)
                    throw new NullReferenceException();

                context.TestCases.Remove(testCase);
                context.SaveChanges();
            }
        }
    }
}
