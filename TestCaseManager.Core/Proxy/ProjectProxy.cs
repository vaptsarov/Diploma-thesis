namespace TestCaseManager.Core.Proxy
{
    using System.Collections.ObjectModel;

    public class ProjectProxy
    {
        public ProjectProxy()
        {
            Areas = new ObservableCollection<AreaProxy>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public ObservableCollection<AreaProxy> Areas { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}