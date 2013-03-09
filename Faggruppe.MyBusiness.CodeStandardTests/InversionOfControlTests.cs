using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;

namespace Faggruppe.MyBusiness.CodeStandardTests
{
    [TestClass]
    public class InversionOfControlTests
    {
        private IProject _projectUnderTest;

        [TestInitialize]
        public void Setup()
        {
            _projectUnderTest = RoslynHelpers.GetProject("Faggruppe.MyBusiness");
        }

        [TestMethod]
        public void InterfaceImplementations_AreNotCreated()
        {
            var documents = _projectUnderTest.Documents;
            var allClasses = RoslynHelpers.GetNode<ClassDeclarationSyntax>(documents);
            var allInterfaces = RoslynHelpers.GetNode<InterfaceDeclarationSyntax>(documents);

            var navnPåInterfacer = from c in allInterfaces select c.Identifier.ToString();

            foreach (var doc in documents)
            {
                var objectCreations = RoslynHelpers.GetNode<ObjectCreationExpressionSyntax>(doc);

                foreach (var objectCreation in objectCreations)
                {
                    var obj = objectCreation;
                    var id = (IdentifierNameSyntax) obj.Type;
                    var className = id.Identifier.ToString();
                    var classNode =
                        (from c in allClasses where c.Identifier.ToString() == className select c).FirstOrDefault();

                    var arverEllerImplementerNoe = classNode.BaseList != null;
                    if (arverEllerImplementerNoe)
                    {
                        var typerSomArvesEllerImplementeres = classNode.BaseList.Types;
                        foreach (var baseKlasseEllerInterface in typerSomArvesEllerImplementeres)
                        {
                            var navnPåBaseKlasseEllerInterface = baseKlasseEllerInterface.ToString();
                            var klasseSomImplementerInterfaceFunnet =
                                navnPåInterfacer.Contains(navnPåBaseKlasseEllerInterface);
                            if (klasseSomImplementerInterfaceFunnet)
                            {
                                var errorMsg =
                                    string.Format(
                                        "Klasse som implementerer et interface skal ikke instansieres. I dokument {0}, instansiering av {1} som implementerer {2}",
                                        doc.Name, className, navnPåBaseKlasseEllerInterface);
                                Assert.Fail(errorMsg);
                            }

                        }

                    }
                }
            }
        }

        [TestMethod]
        public void InterfaceImplementations_BySemanticApi_AreNotCreated()
        {
            foreach (var document in _projectUnderTest.Documents)
            {
                var semanticModel = RoslynHelpers.GetSemanticModel(_projectUnderTest, document);
                var objectCreations = RoslynHelpers.GetNode<ObjectCreationExpressionSyntax>(document);
                foreach (var objCreation in objectCreations)
                {
                    var symbolInfo = semanticModel.GetSymbolInfo(objCreation);
                    var interfaces = symbolInfo.Symbol.ContainingType.Interfaces;
                    if (interfaces.Count > 0)
                    {
                        var interfac = interfaces.First();
                        var errorMsg =
                                   string.Format(
                                       "Klasse som implementerer et interface skal ikke instansieres. I dokument {0}, instansiering av {1} som implementerer {2}",
                                       document.Name, symbolInfo.Symbol.ContainingSymbol.Name, interfac.Name);
                        Assert.Fail(errorMsg);
                    }
                }
             
            }

        }
    }
}
