using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Services;

namespace Faggruppe.MyBusiness.CodeStandardTests
{
    [TestClass]
    public class UnitTest1
    {
        private IProject _projectUnderTest;

        [TestInitialize]
        public void Setup()
        {
            IWorkspace workspace;
            workspace = Workspace.LoadSolution(@"..\..\..\Faggruppe.Roslyn.sln");
            ISolution solution = workspace.CurrentSolution;
            var foundProjectsByName = solution.GetProjectsByName("Faggruppe.MyBusiness");
            _projectUnderTest = foundProjectsByName.FirstOrDefault();
        }

        [TestMethod]
        public void Solution_ShouldContainBusinessProject()
        {
            Assert.IsNotNull(_projectUnderTest);
        }

        [TestMethod]
        public void BusinessProject_EachFile_ContainsAtMostOneClass()
        {
            var classTokenKind = 8325;
            var documents = _projectUnderTest.Documents;
            foreach (var doc in documents)
            {
                var syntaxTree = doc.GetSyntaxTree();
                var root = syntaxTree.GetRoot();
                var allTokensInDocument = root.DescendantTokens();

               
                var numberOfClassKeyWordsInDoc = allTokensInDocument.Count(token => token.Kind == classTokenKind);

                var errorMsg = string.Format("Document {0} contains more than 1 class. Number of class keywords usage {1}", doc.Name, numberOfClassKeyWordsInDoc);
                Assert.IsTrue(numberOfClassKeyWordsInDoc < 2, errorMsg);
            }
        }
    }
}
