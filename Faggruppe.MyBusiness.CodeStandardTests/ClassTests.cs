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
            var documents = _projectUnderTest.Documents;
            foreach (var doc in documents)
            {
                var classes = GetNode<ClassDeclarationSyntax>(doc);
                var numberOfClassKeyWordsInDoc = classes.Count();

                var errorMsg = string.Format("Document {0} contains more than 1 class. Number of class keywords usage {1}", doc.Name, numberOfClassKeyWordsInDoc);
                Assert.IsTrue(numberOfClassKeyWordsInDoc < 2, errorMsg);
            }
        }

        private static IEnumerable<CommonSyntaxNodeOrToken> GetNode<T>(IDocument doc)
        {
            var syntaxTree = doc.GetSyntaxTree();
            var root = syntaxTree.GetRoot();
            var descendants = root.DescendantNodesAndTokens();
            var nodes = from c in descendants where c.AsNode() is T select c;
            return nodes;
        }
    }
}
