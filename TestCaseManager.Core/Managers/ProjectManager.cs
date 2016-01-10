﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class ProjectManager
    {
        public List<Project> GetAll()
        {
            List<Project> projectList = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                projectList = context.Projects.ToList();
            }

            return projectList;
        }

        public Project GetById(int id)
        {
            Project project = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                project = context.Projects.Where(proj => proj.ID == id).FirstOrDefault();
            }

            //TODO: Throw custom exception for null project.
            if (project == null)
                throw new NullReferenceException();

            return project;
        }

        public Project Create(string title)
        {
            Project project = null;
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                project = new Project();
                project.Title = title;
                project.CreatedBy = AuthenticationManager.Instance().GetCurrentUsername ?? "Borislav Vaptsarov";

                context.Projects.Add(project);
                context.SaveChanges();
            }

            return project;
        }

        public void DeleteById(int id)
        {
            using (TestcaseManagerDB context = new TestcaseManagerDB())
            {
                Project project = context.Projects.Where(proj => proj.ID == id).FirstOrDefault();

                if (project == null)
                    throw new NullReferenceException();

                context.Projects.Remove(project);
                context.SaveChanges();
            }
        }
    }
}