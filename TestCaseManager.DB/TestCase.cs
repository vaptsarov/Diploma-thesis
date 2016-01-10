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
    
    public partial class TestCase
    {
        public TestCase()
        {
            this.StepDefinitions = new HashSet<StepDefinition>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Severity { get; set; }
        public bool IsAutomated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int AreaID { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual ICollection<StepDefinition> StepDefinitions { get; set; }
    }
}