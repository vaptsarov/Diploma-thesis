namespace TestCaseManager.Core.Managers.ProxyManagers
{
    using System.Collections.ObjectModel;

    public interface IProxyManager<T>
    {
        ObservableCollection<T> GetAll();
    }
}