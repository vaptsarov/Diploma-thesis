using System.Collections.Generic;

namespace TestCaseManager.Core.Proxy
{
    public class ProjectProxy
    {
        public ProjectProxy(string title)
        {
            Title = title;
            Areas = new List<AreaProxy>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public List<AreaProxy> Areas { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
