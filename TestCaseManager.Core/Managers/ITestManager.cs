using System.Collections.Generic;

namespace TestCaseManager.Core.Managers
{
    internal interface ITestManager<T>
    {
        List<T> GetAll();
        T GetById(int id);
        T Update(T proxy);
        void DeleteById(int id);
    }
}