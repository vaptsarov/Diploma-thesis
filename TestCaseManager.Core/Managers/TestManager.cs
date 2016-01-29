using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.Core.Proxy;
using TestCaseManager.Core.Proxy.TestDefinition;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestManager : ITestManager<TestCase, TestCaseProxy>
    {
        public TestCase Create(int areaId, TestCaseProxy testCase)
        {
            TestCase @case = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                @case = this.MapPrimaryPropertiesFromProxy(testCase, areaId);

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

        public TestCase Update(TestCaseProxy testCase)
        {
            TestCase @case = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                @case = context.TestCases.Where(tc => tc.ID == testCase.Id).FirstOrDefault();
                @case.Title = testCase.Title;
                @case.Severity = testCase.Severity.ToString();
                @case.Priority = testCase.Priority.ToString();
                @case.IsAutomated = testCase.IsAutomated;
                @case.UpdatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";
                
                if (testCase.StepDefinitionList != null)
                {
                    List<StepDefinition> expectedStepDefinitions = new List<StepDefinition>();
                    foreach (StepDefinitionProxy item in testCase.StepDefinitionList)
                    {
                        if (item.Step != null)
                        {
                            StepDefinition definition = context.StepDefinitions.Where(sd => sd.ID == item.ID && sd.TestCaseID == @case.ID).FirstOrDefault();
                            if (definition != null)
                            {
                                definition.Step = item.Step;
                                definition.ExpectedResult = item.ExpectedResult;
                            }
                            else
                            {
                                definition = new StepDefinition();
                                definition.Step = item.Step;
                                definition.ExpectedResult = item.ExpectedResult;
                                definition.TestCaseID = @case.ID;
                                @case.StepDefinitions.Add(definition);
                            }

                            context.SaveChanges();
                            expectedStepDefinitions.Add(definition);
                        }
                    }

                    // Register steps definitions for delete
                    List<StepDefinition> stepsDefinitionsToDelete = new List<StepDefinition>();
                    foreach (StepDefinition item in @case.StepDefinitions)
                    {
                        StepDefinition stepDefinition = expectedStepDefinitions.Where(x => x.ID == item.ID).FirstOrDefault();

                        if (stepDefinition == null)
                            stepsDefinitionsToDelete.Add(item);
                    }

                    // Delete orphans
                    foreach (StepDefinition orphan in stepsDefinitionsToDelete)
                    {
                        context.StepDefinitions.Remove(orphan);
                    }
                }

                context.SaveChanges();
            }

            return @case;
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

        private TestCase MapPrimaryPropertiesFromProxy(TestCaseProxy proxy, int areaId)
        {
            TestCase testCase = new TestCase();
            testCase.Title = proxy.Title;
            testCase.AreaID = areaId;
            testCase.Severity = proxy.Severity.ToString();
            testCase.Priority = proxy.Priority.ToString();
            testCase.IsAutomated = proxy.IsAutomated;
            testCase.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";

            return testCase;
        }
    }
}
