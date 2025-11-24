using System.Reflection;

namespace Lista6;

public class Zadanie4
{
    public static void Zad4()
    {
        var person = new
        {
            firstName = "John",
            lastName = "Egbert",
            age = 13,
            pay = 4500.50
        };

        PrintInfo(person);
    }

    private static void PrintInfo(dynamic person)
    {
        Console.WriteLine($"1: {person.firstName}, {person.lastName}, {person.age}, {person.pay}");

        string firstName = person.firstName;
        string lastName = person.lastName;
        int age = person.age;
        double pay = person.pay;

        Console.WriteLine($"2: {firstName}, {lastName}, {age}, {pay}");

        Console.Write("3: ");
        PropertyInfo[] properties = person.GetType().GetProperties();
        foreach (var prop in properties)
        {;
            Console.Write($"{prop.Name}: ");
            Console.Write($"{prop.GetValue(person)}, ");
        }
        Console.WriteLine();
    }
}