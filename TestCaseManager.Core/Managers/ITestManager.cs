using System.Collections.Generic;

namespace TestCaseManager.Core.Managers
{
    interface ITestManager<T, P>
    {
        List<T> GetAll();
        T GetById(int id);
        T Update(P proxy);
        void DeleteById(int id);
    }
}
