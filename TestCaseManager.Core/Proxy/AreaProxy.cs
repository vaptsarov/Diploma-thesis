using System.Collections.Generic;

namespace TestCaseManager.Core.Proxy
{
    public class AreaProxy
    {
        public AreaProxy()
        {
            TestCasesList = new List<TestCaseProxy>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public List<TestCaseProxy> TestCasesList { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }\
    }
}
