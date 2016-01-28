using System.Collections.Generic;

namespace TestCaseManager.Core.Managers
{
    interface ITestManager<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Update(int id, string title);
        void DeleteById(int id);
    }
}
