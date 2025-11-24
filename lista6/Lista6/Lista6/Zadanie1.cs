namespace Lista6;

class Zadanie1
{
    public static void Zad1()
    {
        var person = (firstName: "John", lastName: "Egbert", age: 13, pay: 6000);

        PrintInfo(person);
    }

    static void PrintInfo((string firstName, string lastName, int age, double pay) person)
    {
        Console.WriteLine($"Sposób 1: {person.firstName} {person.lastName}, wiek: {person.age}, płaca: {person.pay}");

        Console.WriteLine($"Sposób 2: {person.Item1} {person.Item2}, wiek: {person.Item3}, płaca: {person.Item4}");

        var (firstName, lastName, age, pay) = person;
        Console.WriteLine($"Sposób 3: {firstName} {lastName}, wiek: {age}, płaca: {pay}");
    }
}