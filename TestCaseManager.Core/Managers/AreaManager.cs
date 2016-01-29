using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.Core.Proxy;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class AreaManager : ITestManager<Area, AreaProxy>
    {
        public List<Area> GetAll()
        {
            List<Area> areaList = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                areaList = context.Areas.ToList();
            }

            return areaList;
        }

        public Area GetById(int id)
        {
            Area area = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                area = context.Areas.Where(a => a.ID == id).FirstOrDefault();
            }

            //TODO: Throw custom exception for null project.
            if (area == null)
                throw new NullReferenceException();

            return area;
        }

        public Area Create(string title, int projectId)
        {
            Area area = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                area = new Area();
                area.Title = title;
                area.ProjectId = projectId;
                area.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";

                context.Areas.Add(area);
                context.SaveChanges();
            }

            return area;
        }

        public Area Update(AreaProxy proxy)
        {
            Area area = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                area = context.Areas.Where(x => x.ID == proxy.ID).FirstOrDefault();

                if (area == null)
                    throw new NullReferenceException();

                area.Title = proxy.Title;
                area.UpdatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";
                context.SaveChanges();
            }

            return area;
        }

        public void DeleteById(int id)
        {
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                Area area = context.Areas.Where(a => a.ID == id).FirstOrDefault();

                if (area == null)
                    throw new NullReferenceException();

                context.Areas.Remove(area);
                context.SaveChanges();
            }
        }
    }
}
