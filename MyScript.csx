using System;

//http://blogs.msdn.com/b/visualstudio/archive/2011/10/19/introducing-the-microsoft-roslyn-ctp.aspx
public class Person 
{
	public string Speak()
	{
		return "Hello world!";
	}
}

var person = new Person();
for(int i =1; i < 6; i++)
{
	Console.WriteLine(person.Speak());
}