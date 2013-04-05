#r "Faggruppe.MyBusiness.dll"

using System;
using Faggruppe.MyBusiness;

//http://blogs.msdn.com/b/visualstudio/archive/2011/10/19/introducing-the-microsoft-roslyn-ctp.aspx
var person = new Person();
for(int i =1; i < 6; i++)
{
	var person = new Person();
	var spoken = person.Speak();
	Console.WriteLine(spoken);
}