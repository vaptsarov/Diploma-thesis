using System.Collections.Generic;

namespace TestCaseManager.Core.Proxy
{
    public class AreaProxy
    {
        public AreaProxy()
        {
            TestCasesList = new List<TestCaseProxy>();
        }

        public string Name { get; set; }

        public List<TestCaseProxy> TestCasesList { get; set; }
    }
}
