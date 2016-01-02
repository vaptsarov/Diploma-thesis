using System.Collections.Generic;

namespace TestCaseManager.Core.Proxy
{
    public class ProjectProxy
    {
        public ProjectProxy(string projectName)
        {
            ProjectName = projectName;
            Areas = new List<AreaProxy>();
        }

        public string ProjectName { get; set; }
        public List<AreaProxy> Areas { get; set; }
    }
}
