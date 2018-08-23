using System.Collections.Generic;
using CRM.Workshops.Workflows;
using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CRM.Workshops.Tests
{
    /// <summary>
    /// https://dynamicsvalue.com/get-started/codeactivities 
    /// </summary>
    [TestClass]
    public class WorkflowTest
    {
        [TestMethod]
        public void When_the_add_activity_is_executed_the_right_sum_is_returned()
        {
            var fakedContext = new XrmFakedContext();

            //Inputs
            var inputs = new Dictionary<string, object>() {
                { "FirstSummand", 2 },
                { "SecondSummand", 3 }
            };

            var result = fakedContext.ExecuteCodeActivity<AddActivity>(inputs);

            Assert.IsTrue(((int) result["Result"]).Equals(5));
        }           
    }
}