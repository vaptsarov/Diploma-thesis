using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestCaseManager.Core.Proxy
{
    public class AreaProxy
    {
        public AreaProxy()
        {
            TestCasesList = new ObservableCollection<TestCaseProxy>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public ObservableCollection<TestCaseProxy> TestCasesList { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
