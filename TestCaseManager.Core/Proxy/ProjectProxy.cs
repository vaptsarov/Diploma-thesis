using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestCaseManager.Core.Proxy
{
    public class ProjectProxy
    {
        public ProjectProxy()
        {
            Areas = new ObservableCollection<AreaProxy>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public ObservableCollection<AreaProxy> Areas { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
