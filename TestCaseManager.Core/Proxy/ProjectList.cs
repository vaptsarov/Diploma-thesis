using System.Collections.Generic;

namespace TestCaseManager.Core.Proxy
{
    public class ProjectList : List<ProjectProxy>
    {
        public ProjectList()
        {
        }

        public ProjectList(List<ProjectProxy> projList)
        {
            projList.ForEach(x =>
            {
                this.Add(x);
            });
        }
    }
}
