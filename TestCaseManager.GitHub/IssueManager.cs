using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using TestCaseManager.Core.Managers;
using TestCaseManager.DB;
using TestCaseManager.Utilities.StringUtility;

namespace TestCaseManager.GitHub
{
    public class IssueManager
    {
        private static IGitHubClient _client;
        private static IssueManager _instance;

        public static IssueManager Instance(string username, SecureString password)
        {
            _instance = new IssueManager();
            SetContextUser(username, password);

            return _instance;
        }

        public List<Tuple<string, string>> GetRepositories()
        {
            var task = GetRepositories(_client);
            var repo = task.Result;

            return repo.Where(y => (y.Permissions.Pull || y.Permissions.Push || y.Permissions.Admin) && y.Fork == false)
                .Select(x => new Tuple<string, string>(x.Owner.Login, x.Name)).ToList();
        }

        public Uri CreateIssue(string ownerName, string repositoryName, int testCaseId)
        {
            var manager = new TestManager();
            var testCase = manager.GetById(testCaseId);

            var issue = PopulateIssue(testCase);

            var task = CreateGitHubIssue(_client, ownerName, repositoryName, issue);
            var createdIssue = task.Result;

            return new Uri(createdIssue.HtmlUrl);
        }

        private static async Task<Issue> CreateGitHubIssue(IGitHubClient client, string ownerName, string repositoryName,
            NewIssue issue)
        {
            return await client.Issue.Create(ownerName, repositoryName, issue);
        }

        private static async Task<IReadOnlyList<Repository>> GetRepositories(IGitHubClient client)
        {
            return await client.Repository.GetAllForCurrent();
        }

        private static void SetContextUser(string username, SecureString password)
        {
            _client = new GitHubClient(new ProductHeaderValue("CustomApplication"))
            {
                Credentials = new Credentials(username, password.ConvertToUnsecureString())
            };
        }

        private static NewIssue PopulateIssue(TestCase testCase)
        {
            var issue = new NewIssue($"[Bug] {testCase.Title}");
            StringBuilder bodyBuilder = bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("## Steps to reproduce");
            bodyBuilder.AppendLine();

            var counter = 1;
            foreach (var stepDefinition in new TestManager().GetStepDefinitionsById(testCase.ID))
            {
                if (string.IsNullOrWhiteSpace(stepDefinition.ExpectedResult) == false)
                    bodyBuilder.AppendLine(
                        $"{counter}. {stepDefinition.Step} - *Expected result:* {stepDefinition.ExpectedResult}");
                else
                    bodyBuilder.AppendLine($"{counter}. {stepDefinition.Step}");

                counter++;
            }

            bodyBuilder.AppendLine();
            bodyBuilder.AppendLine("## Additinal info");
            bodyBuilder.AppendLine();
            bodyBuilder.AppendLine($"**Test case ID :** {testCase.ID}");
            bodyBuilder.AppendLine($"**Severity :** {testCase.Severity}");
            bodyBuilder.AppendLine($"**Priority :** {testCase.Priority}");
            bodyBuilder.AppendLine($"**Created by :** {testCase.CreatedBy}");
            bodyBuilder.AppendLine($"**Is Automated :** {testCase.IsAutomated}");

            issue.Body = bodyBuilder.ToString();

            return issue;
        }
    }
}