﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class AreaManager
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

        public void DeleteById(int id)
        {
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                Area project = context.Areas.Where(proj => proj.ID == id).FirstOrDefault();

                if (project == null)
                    throw new NullReferenceException();

                context.Areas.Remove(project);
                context.SaveChanges();
            }
        }
    }
}