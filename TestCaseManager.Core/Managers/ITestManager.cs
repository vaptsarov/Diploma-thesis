using System.Collections.Generic;

namespace TestCaseManager.Core.Managers
{
    interface ITestManager<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Update(int itemId, string title);
        void DeleteById(int id);
    }
}
