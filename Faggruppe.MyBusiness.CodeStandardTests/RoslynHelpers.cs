using System.Collections.Generic;
using System.Linq;
using Roslyn.Compilers.CSharp;
using Roslyn.Compilers.Common;
using Roslyn.Services;

namespace Faggruppe.MyBusiness.CodeStandardTests
{
    public class RoslynHelpers
    {
        public static IEnumerable<T> GetNode<T>(IDocument doc) where T : SyntaxNode
        {
            var syntaxTree = doc.GetSyntaxTree();
            var root = syntaxTree.GetRoot();
            var descendants = root.DescendantNodes();
            var nodes = from c in descendants where c is T select c as T;
            return nodes;
        }

        public static IProject GetProject(string projectName)
        {
            IWorkspace workspace = Workspace.LoadSolution(@"..\..\..\Faggruppe.Roslyn.sln");
            
            ISolution solution = workspace.CurrentSolution;
            var foundProjectsByName = solution.GetProjectsByName(projectName);
            return foundProjectsByName.Single();
        }

        public static IEnumerable<T> GetNode<T>(IEnumerable<IDocument> documents) where T : SyntaxNode
        {
            var listOfNodes = new List<T>();
            foreach (var doc in documents)
            {
                var syntaxTree = doc.GetSyntaxTree();
                var root = syntaxTree.GetRoot();
                var descendants = root.DescendantNodes();
                var nodes = from c in descendants where c is T select c as T;
                listOfNodes.AddRange(nodes);
            }
          
            return listOfNodes;
        }

        public static ISemanticModel GetSemanticModel(IProject project, IDocument doc)
        {
            var comp = project.GetCompilation();

            CommonSyntaxTree tree = doc.GetSyntaxRoot().SyntaxTree;
            return comp.GetSemanticModel(tree);
        }
    }
}