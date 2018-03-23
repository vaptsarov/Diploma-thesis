using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TestCaseManager.Core.Managers;
using TestCaseManager.DB;
using TestCaseManager.Utilities;

namespace TestCaseManager.GitHub
{
    public class IssueManager
    {
        private static GitHubClient client;
        private static IssueManager instance = null;

        public static IssueManager Instance(string username, SecureString password)
        {
            instance = new IssueManager();
            instance.SetContextUser(username, password);

            return instance;
        }

        public List<Tuple<string, string>> GetRepositories()
        {
            Task<IReadOnlyList<Repository>> task = this.GetRepositories(client);
            var repo = task.Result;

            return repo.Where(y=>
                (y.Permissions.Pull == true || y.Permissions.Push == true || y.Permissions.Admin == true) && y.Fork == false)
                .Select(x => new Tuple<string, string>(x.Owner.Login, x.Name)).ToList<Tuple<string, string>>();
        }

        public Uri CreateIssue(string ownerName, string repositoryName, int testCaseId)
        {
            TestManager manager = new TestManager();
            TestCase testCase = manager.GetById(testCaseId);

            NewIssue issue = this.PopulateIssue(testCase);

            Task<Issue> task = this.CreateGitHubIssue(client, ownerName, repositoryName, issue);
            Issue createdIssue = task.Result;

            return new Uri(createdIssue.HtmlUrl);
        }

        private async Task<Issue> CreateGitHubIssue(GitHubClient client, string ownerName, string repositoryName, NewIssue issue)
        {
            return await client.Issue.Create(ownerName, repositoryName, issue);
        }

        private async Task<IReadOnlyList<Repository>> GetRepositories(GitHubClient client)
        {
            return await client.Repository.GetAllForCurrent();
        }

        private void SetContextUser(string username, SecureString password)
        {
            client = new GitHubClient(new ProductHeaderValue("CustomApplication"));
            client.Credentials = new Credentials(username, password.ConvertToUnsecureString());
        }

        private NewIssue PopulateIssue(TestCase testCase)
        {
            NewIssue issue = new NewIssue(string.Format("[Bug] {0}", testCase.Title));
            StringBuilder bodyBuilder = bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("## Steps to reproduce");
            bodyBuilder.AppendLine();

            int counter = 1;
            foreach (var stepDefinition in new TestManager().GetStepDefinitionsById(testCase.ID))
            {
                if (string.IsNullOrWhiteSpace(stepDefinition.ExpectedResult) == false)
                    bodyBuilder.AppendLine(string.Format("{0}. {1} - *Expected result:* {2}", counter, stepDefinition.Step, stepDefinition.ExpectedResult));
                else
                    bodyBuilder.AppendLine(string.Format("{0}. {1}", counter, stepDefinition.Step));

                counter++;
            }

            bodyBuilder.AppendLine();
            bodyBuilder.AppendLine("## Additinal info");
            bodyBuilder.AppendLine();
            bodyBuilder.AppendLine(string.Format("**Test case ID :** {0}", testCase.ID));
            bodyBuilder.AppendLine(string.Format("**Severity :** {0}", testCase.Severity));
            bodyBuilder.AppendLine(string.Format("**Priority :** {0}", testCase.Priority));
            bodyBuilder.AppendLine(string.Format("**Created by :** {0}", testCase.CreatedBy));
            bodyBuilder.AppendLine(string.Format("**Is Automated :** {0}", testCase.IsAutomated));

            issue.Body = bodyBuilder.ToString();

            return issue;
        }
    }
}
