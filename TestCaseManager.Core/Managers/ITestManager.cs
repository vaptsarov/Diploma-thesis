namespace TestCaseManager.Core.Managers
{
    using System.Collections.Generic;

    internal interface ITestManager<T>
    {
        List<T> GetAll();
        T GetById(int id);
        T Update(T proxy);
        void DeleteById(int id);
    }
}