﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestCaseManager.Core.AuthenticatePoint;
using TestCaseManager.DB;

namespace TestCaseManager.Core.Managers
{
    public class ProjectManager : ITestManager<Project>
    {
        public List<Project> GetAll()
        {
            List<Project> projectList = null;
            using (var context = new TestcaseManagerDB())
            {
                projectList = context.Projects.ToList();
            }

            return projectList;
        }

        public Project GetById(int id)
        {
            Project project = null;
            using (var context = new TestcaseManagerDB())
            {
                project = context.Projects.FirstOrDefault(proj => proj.ID == id);
            }

            //TODO: Throw custom exception for null project.
            if (project == null)
                throw new NullReferenceException();

            return project;
        }

        public Project Update(Project project)
        {
            Project projectToUpdate = null;
            using (var context = new TestcaseManagerDB())
            {
                projectToUpdate = context.Projects.FirstOrDefault(proj => proj.ID == project.ID);
                if (projectToUpdate == null)
                    throw new NullReferenceException();

                projectToUpdate.Title = project.Title;
                projectToUpdate.UpdatedBy = AuthenticationManager.Instance().GetCurrentUsername;
                context.SaveChanges();
            }

            return projectToUpdate;
        }

        public void DeleteById(int id)
        {
            using (var context = new TestcaseManagerDB())
            {
                var project = context.Projects.FirstOrDefault(proj => proj.ID == id);

                if (project == null)
                    throw new NullReferenceException();

                context.Projects.Remove(project);
                context.SaveChanges();
            }
        }

        public Project Create(string title)
        {
            Project project = null;
            using (var context = new TestcaseManagerDB())
            {
                project = new Project
                {
                    Title = title,
                    CreatedBy = AuthenticationManager.Instance().GetCurrentUsername
                };

                context.Projects.Add(project);
                context.SaveChanges();
            }

            return project;
        }
    }
}