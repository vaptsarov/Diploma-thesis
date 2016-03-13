using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestManager : ITestManager<TestCase>
    {
        public TestCase Create(int areaId, TestCase testCase)
        {
            TestCase @case = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                @case = this.MapPrimaryProperties(testCase);
                @case.AreaID = areaId;
                @case.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";

                context.TestCases.Add(@case);
                context.SaveChanges();

                if (testCase.StepDefinitions != null)
                {
                    foreach (StepDefinition item in testCase.StepDefinitions)
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

        public ICollection<StepDefinition> GetStepDefinitionsById(int testCaseId)
        {
            ICollection<StepDefinition> stepDefinitions = new Collection<StepDefinition>();
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                var @case = context.TestCases.Where(x => x.ID == testCaseId).FirstOrDefault();

                if (@case.StepDefinitions != null && @case.StepDefinitions.Count() > 0)
                    stepDefinitions = @case.StepDefinitions;
            }

            return stepDefinitions;
        }

        public TestCase Update(TestCase testCase)
        {
            TestCase @case = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                @case = context.TestCases.Where(tc => tc.ID == testCase.ID).FirstOrDefault();

                @case.Title = testCase.Title;
                @case.Severity = testCase.Severity;
                @case.Priority = testCase.Priority;
                @case.IsAutomated = testCase.IsAutomated;
                @case.UpdatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";

                if (testCase.StepDefinitions != null)
                {
                    List<StepDefinition> expectedStepDefinitions = new List<StepDefinition>();
                    foreach (StepDefinition item in testCase.StepDefinitions)
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

                TestComposite composite = context.TestComposites.Where(comp => comp.TestCaseID == id).FirstOrDefault();
                if (composite != null)
                    context.TestComposites.Remove(composite);
                
                context.TestCases.Remove(testCase);
                context.SaveChanges();
            }
        }

        private TestCase MapPrimaryProperties(TestCase item)
        {
            TestCase testCase = new TestCase();
            testCase.Title = item.Title;
            testCase.Severity = item.Severity;
            testCase.Priority = item.Priority;
            testCase.IsAutomated = item.IsAutomated;

            return testCase;
        }
    }
}
