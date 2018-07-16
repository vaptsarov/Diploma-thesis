using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestCaseManager.Core.AuthenticatePoint;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class TestManager : ITestManager<TestCase>
    {
        public List<TestCase> GetAll()
        {
            List<TestCase> testCasesList = null;
            using (var context = new TestcaseManagerDB())
            {
                testCasesList = context.TestCases.ToList();
            }

            return testCasesList;
        }

        public TestCase GetById(int id)
        {
            TestCase testCase = null;
            using (var context = new TestcaseManagerDB())
            {
                testCase = context.TestCases.FirstOrDefault(tc => tc.ID == id);
            }

            //TODO: Throw custom exception for null project.
            if (testCase == null)
                throw new NullReferenceException();

            return testCase;
        }

        public TestCase Update(TestCase testCase)
        {
            TestCase @case = null;
            using (var context = new TestcaseManagerDB())
            {
                @case = context.TestCases.FirstOrDefault(tc => tc.ID == testCase.ID);

                @case.Title = testCase.Title;
                @case.Severity = testCase.Severity;
                @case.Priority = testCase.Priority;
                @case.IsAutomated = testCase.IsAutomated;
                @case.UpdatedBy = AuthenticationManager.Instance().GetCurrentUsername;

                if (testCase.StepDefinitions != null)
                {
                    var expectedStepDefinitions = new List<StepDefinition>();
                    foreach (var item in testCase.StepDefinitions)
                        if (item.Step != null)
                        {
                            var definition = context.StepDefinitions.FirstOrDefault(sd => sd.ID == item.ID && sd.TestCaseID == @case.ID);
                            if (definition != null)
                            {
                                definition.Step = item.Step;
                                definition.ExpectedResult = item.ExpectedResult;
                            }
                            else
                            {
                                definition = new StepDefinition
                                {
                                    Step = item.Step,
                                    ExpectedResult = item.ExpectedResult,
                                    TestCaseID = @case.ID
                                };
                                @case.StepDefinitions.Add(definition);
                            }

                            context.SaveChanges();
                            expectedStepDefinitions.Add(definition);
                        }

                    // Register steps definitions for delete
                    var stepsDefinitionsToDelete = new List<StepDefinition>();
                    foreach (var item in @case.StepDefinitions)
                    {
                        var stepDefinition = expectedStepDefinitions.FirstOrDefault(x => x.ID == item.ID);

                        if (stepDefinition == null)
                            stepsDefinitionsToDelete.Add(item);
                    }

                    // Delete orphans
                    foreach (var orphan in stepsDefinitionsToDelete) context.StepDefinitions.Remove(orphan);
                }

                context.SaveChanges();
            }

            return @case;
        }

        public void DeleteById(int id)
        {
            using (var context = new TestcaseManagerDB())
            {
                var testCase = context.TestCases.FirstOrDefault(tc => tc.ID == id);

                if (testCase == null)
                    throw new NullReferenceException();

                IList<TestComposite> composites = context.TestComposites.Where(comp => comp.TestCaseID == id).ToList();
                if (composites.Any())
                    foreach (var item in composites)
                    {
                        context.TestComposites.Remove(item);
                        context.SaveChanges();
                    }

                context.TestCases.Remove(testCase);
                context.SaveChanges();
            }
        }

        public TestCase Create(int areaId, TestCase testCase)
        {
            TestCase @case = null;
            using (var context = new TestcaseManagerDB())
            {
                @case = MapPrimaryProperties(testCase);
                @case.AreaID = areaId;
                @case.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername;

                context.TestCases.Add(@case);
                context.SaveChanges();

                if (testCase.StepDefinitions != null)
                {
                    foreach (var item in testCase.StepDefinitions)
                        if (item.Step != null)
                        {
                            var definition = new StepDefinition
                            {
                                Step = item.Step,
                                ExpectedResult = item.ExpectedResult,
                                TestCaseID = @case.ID
                            };
                            @case.StepDefinitions.Add(definition);
                        }

                    context.SaveChanges();
                }
            }

            return @case;
        }

        public ICollection<StepDefinition> GetStepDefinitionsById(int testCaseId)
        {
            ICollection<StepDefinition> stepDefinitions = new Collection<StepDefinition>();
            using (var context = new TestcaseManagerDB())
            {
                var @case = context.TestCases.FirstOrDefault(x => x.ID == testCaseId);

                if (@case?.StepDefinitions != null && @case.StepDefinitions.Any())
                    stepDefinitions = @case.StepDefinitions;
            }

            return stepDefinitions;
        }

        private static TestCase MapPrimaryProperties(TestCase item)
        {
            var testCase = new TestCase
            {
                Title = item.Title,
                Severity = item.Severity,
                Priority = item.Priority,
                IsAutomated = item.IsAutomated
            };

            return testCase;
        }
    }
}