//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TestComposite
    {
        public int TestRunID { get; set; }
        public int TestCaseID { get; set; }
        public string TestCaseStatus { get; set; }
    
        public virtual TestCase TestCas { get; set; }
        public virtual TestRun TestRun { get; set; }
    }
}
