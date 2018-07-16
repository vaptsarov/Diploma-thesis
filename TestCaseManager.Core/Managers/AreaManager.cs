using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.Core.AuthenticatePoint;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class AreaManager : ITestManager<Area>
    {
        public List<Area> GetAll()
        {
            List<Area> areaList = null;
            using (var context = new TestcaseManagerDB())
            {
                areaList = context.Areas.ToList();
            }

            return areaList;
        }

        public Area GetById(int id)
        {
            Area area = null;
            using (var context = new TestcaseManagerDB())
            {
                area = context.Areas.FirstOrDefault(a => a.ID == id);
            }

            //TODO: Throw custom exception for null project.
            if (area == null)
                throw new NullReferenceException();

            return area;
        }

        public Area Update(Area area)
        {
            Area areaToUpdate = null;
            using (var context = new TestcaseManagerDB())
            {
                areaToUpdate = context.Areas.FirstOrDefault(x => x.ID == area.ID);
                if (areaToUpdate == null)
                    throw new NullReferenceException();

                areaToUpdate.Title = area.Title;
                areaToUpdate.UpdatedBy = AuthenticationManager.Instance().GetCurrentUsername;
                context.SaveChanges();
            }

            return areaToUpdate;
        }

        public void DeleteById(int id)
        {
            using (var context = new TestcaseManagerDB())
            {
                var area = context.Areas.FirstOrDefault(a => a.ID == id);

                if (area == null)
                    throw new NullReferenceException();

                context.Areas.Remove(area);
                context.SaveChanges();
            }
        }

        public Area Create(string title, int projectId)
        {
            Area area = null;
            using (var context = new TestcaseManagerDB())
            {
                area = new Area
                {
                    Title = title,
                    ProjectId = projectId,
                    CreatedBy = AuthenticationManager.Instance().GetCurrentUsername
                };

                context.Areas.Add(area);
                context.SaveChanges();
            }

            return area;
        }
    }
}