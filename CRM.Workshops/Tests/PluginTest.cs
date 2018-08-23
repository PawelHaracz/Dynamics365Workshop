using System;
using CRM.Workshops.Plugins;
using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace CRM.Workshops.Tests
{
    [TestClass]
    public class PluginTest
    {
        [TestMethod]
        public void When_the_account_number_plugin_is_executed_it_adds_a_random_number_to_an_account_entity()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            var target = new Entity("account") { Id = guid1 };

            //Execute our plugin against a target that doesn't contains the accountnumber attribute
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<SimplePlugin>(target);

            //Assert that the target contains a new attribute
            Assert.IsTrue(target.Attributes.ContainsKey("accountnumber"));

        }
    }
}