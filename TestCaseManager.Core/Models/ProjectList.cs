using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.Core.Models
{
    public class ProjectList : List<Project>
    {
        public ProjectList()
        {
        }

        public ProjectList(List<Project> projList)
        {
            projList.ForEach(x =>
            {
                this.Add(x);
            });
        }
    }
}
