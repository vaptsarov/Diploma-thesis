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
    
    public partial class StepDefinition
    {
        public int ID { get; set; }
        public string Step { get; set; }
        public string ExpectedResult { get; set; }
        public int TestCaseID { get; set; }
    
        public virtual TestCas TestCas { get; set; }
    }
}