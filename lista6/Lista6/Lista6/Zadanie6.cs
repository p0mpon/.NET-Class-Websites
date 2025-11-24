namespace Lista6;

public class Zadanie6
{
    public static void Zad6()
    {
        var wynik = CountTypes(10, 3, 2.5, -3.1, "hedera helix", "fox", true, false, 
            14.2, 8, (firstName: "John", lastName: "Egbert", age: 13, pay: 6000));

        Console.WriteLine($"Parzyste int: {wynik.Evens}");
        Console.WriteLine($"Dodatnie double: {wynik.PositiveDoubles}");
        Console.WriteLine($"Napisy ponad 5 znaków: {wynik.LongStrings}");
        Console.WriteLine($"Inne typy: {wynik.Others}");
    }

    static (int Evens, int PositiveDoubles, int LongStrings, int Others) CountTypes(params object[] args)
    {
        int evens = 0;
        int posDoubles = 0;
        int longStrings = 0;
        int others = 0;

        foreach (var item in args)
        {
            switch (item)
            {
                case int x when x % 2 == 0:
                    evens++;
                    break;

                case double d when d > 0:
                    posDoubles++;
                    break;

                case string s when s.Length >= 5:
                    longStrings++;
                    break;

                default:
                    others++;
                    break;
            }
        }

        return (evens, posDoubles, longStrings, others);
    }
}