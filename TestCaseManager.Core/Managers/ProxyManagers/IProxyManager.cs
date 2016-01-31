using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TestCaseManager.Core.Managers.ProxyManagers
{
    public interface IProxyManager<T>
    {
        ObservableCollection<T> GetAll();
    }
}
