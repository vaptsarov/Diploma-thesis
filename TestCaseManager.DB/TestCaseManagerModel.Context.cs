﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestCaseManager.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestcaseManagerDB : DbContext
    {
        public TestcaseManagerDB()
            : base("name=TestcaseManagerDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<StepDefinition> StepDefinitions { get; set; }
        public DbSet<TestCas> TestCases { get; set; }
    }
}