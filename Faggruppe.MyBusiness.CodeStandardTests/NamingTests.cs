using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;

namespace Faggruppe.MyBusiness.CodeStandardTests
{
    [TestClass]
    public class NamingTests
    {
        private IProject _projectUnderTest;

        [TestInitialize]
        public void Setup()
        {   
            _projectUnderTest = RoslynHelpers.GetProject("Faggruppe.MyBusiness");
        }

        [TestMethod]
        public void Methods_StartsWithUpperCaseLetter()
        {
            var documents = _projectUnderTest.Documents;
            foreach (var doc in documents)
            {
                var methods = RoslynHelpers.GetNode<MethodDeclarationSyntax>(doc);
              
                foreach (var method in methods)
                {
                    var methodName = method.Identifier;
                    var firstLetterOfName = methodName.ValueText.First();
                    var isFirstCharUpper = char.IsUpper(firstLetterOfName);
                    var errorMsg = string.Format("Document {0} contains method with illegal method naming. Method : {1}", doc.Name, methodName);
                    Assert.IsTrue(isFirstCharUpper, errorMsg);
                }
             
            }
        }
    }
}
