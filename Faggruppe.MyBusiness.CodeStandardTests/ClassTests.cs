using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using Roslyn.Compilers.Common;
using Roslyn.Services;

namespace Faggruppe.MyBusiness.CodeStandardTests
{
    [TestClass]
    public class ClassTests
    {
        private IProject _projectUnderTest;

        [TestInitialize]
        public void Setup()
        {   
            _projectUnderTest = RoslynHelpers.GetProject("Faggruppe.MyBusiness");
        }

        [TestMethod]
        public void Solution_ShouldContainBusinessProject()
        {
            Assert.IsNotNull(_projectUnderTest);
        }

        [TestMethod]
        public void BusinessProject_EachFile_ContainsAtMostOneClass()
        {
            var documents = _projectUnderTest.Documents;
            foreach (var doc in documents)
            {
                var classes = RoslynHelpers.GetNode<ClassDeclarationSyntax>(doc);
                var numberOfClassKeyWordsInDoc = classes.Count();

                var errorMsg = string.Format("Document {0} contains more than 1 class. Number of class keywords usage {1}", doc.Name, numberOfClassKeyWordsInDoc);
                Assert.IsTrue(numberOfClassKeyWordsInDoc < 2, errorMsg);
            }
        }
    }
}
