namespace Lista6;

public class Zadanie3
{
    public static void Zad3()
    {
        Fill();
        Reverse();
        FindAll();
        Sort();
        Exists();
    }

    static void Fill()
    {
        int[] array = new int[5];
        Console.WriteLine("Fill(): " + string.Join(", ", array));
        
        Array.Fill(array, 1);
        Console.WriteLine("Fill(): " + string.Join(", ", array));
        Console.WriteLine();
    }

    static void Reverse()
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8};
        Console.WriteLine("Reverse(): " + string.Join(", ", array));
        
        Array.Reverse(array);
        Console.WriteLine("Reverse(): " + string.Join(", ", array));
        Console.WriteLine();
    }

    static void FindAll()
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.WriteLine("FindAll(): " + string.Join(", ", array));
        
        int[] even = Array.FindAll(array, x => x % 2 == 0);
        Console.WriteLine("FindAll(): " + string.Join(", ", even));
        Console.WriteLine();
    }

    static void Sort()
    {
        int[] array = { 5, 1, 4, 7, 2, 3, 8, 6};
        Console.WriteLine("Sort(): " + string.Join(", ", array));
        
        Array.Sort(array);
        Console.WriteLine("Sort(): " + string.Join(", ", array));
        Console.WriteLine();
    }

    static void Exists()
    {
        int[] array = { 3, 7, 10, 15, 21 };
        Console.WriteLine("Exists(): " + string.Join(", ", array));

        bool isEven = Array.Exists(array, x => x % 2 == 0);
        Console.WriteLine("Exists(): " + isEven);

    }
}