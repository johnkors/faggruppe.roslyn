using System.Collections.Generic;
using System.Linq;
using Roslyn.Compilers.Common;
using Roslyn.Services;

namespace Faggruppe.MyBusiness.CodeStandardTests
{
    public class RoslynHelpers
    {
        public static IEnumerable<CommonSyntaxNodeOrToken> GetNode<T>(IDocument doc)
        {
            var syntaxTree = doc.GetSyntaxTree();
            var root = syntaxTree.GetRoot();
            var descendants = root.DescendantNodesAndTokens();
            var nodes = from c in descendants where c.AsNode() is T select c;
            return nodes;
        }

        public static IProject GetProject(string projectName)
        {
            IWorkspace workspace = Workspace.LoadSolution(@"..\..\..\Faggruppe.Roslyn.sln");
            ISolution solution = workspace.CurrentSolution;
            var foundProjectsByName = solution.GetProjectsByName(projectName);
            return foundProjectsByName.FirstOrDefault();
        }
    }
}