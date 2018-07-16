using System.Collections.ObjectModel;

namespace TestCaseManager.Core.Managers.ProxyManagers
{
    public interface IProxyManager<T>
    {
        ObservableCollection<T> GetAll();
    }
}