using System.Collections.Generic;

namespace TestCaseManager.Core.Managers
{
    interface ITestManager<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void DeleteById(int id);
    }
}
