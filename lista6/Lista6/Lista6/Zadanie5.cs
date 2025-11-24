namespace Lista6;

public class Zadanie5
{
    public static void Zad5()
    {
        DrawCard("Ryszard", "Rys", 'X', 2, 20);
        Console.WriteLine();
        
        DrawCard("Patrycja");
        Console.WriteLine();

        DrawCard(
            firstLine: "Izabela",
            secondLine: "Lekarka",
            borderChar: '#',
            borderWidth: 1,
            minWidth: 25
        );
        Console.WriteLine();

        DrawCard("Hatsune Miku", borderChar: '*', borderWidth: 5);
    }


    static void DrawCard(
        string firstLine,
        string secondLine = "",
        char borderChar = '#',
        int borderWidth = 1,
        int minWidth = 20)
    {
        int innerWidth = Math.Max(
            Math.Max(firstLine.Length, secondLine.Length) + 2,
            minWidth - 2 * borderWidth
        );

        int totalWidth = innerWidth + 2 * borderWidth;

        void DrawBorder()
        {
            Console.WriteLine(new string(borderChar, totalWidth));
        }

        void DrawCentered(string text)
        {
            int spaces = innerWidth - text.Length;
            int left = spaces / 2;
            int right = spaces - left;

            Console.WriteLine(
                new string(borderChar, borderWidth) +
                new string(' ', left) +
                text +
                new string(' ', right) +
                new string(borderChar, borderWidth)
            );
        }

        for (int i = 0; i < borderWidth; i++)
            DrawBorder();

        DrawCentered(firstLine);
        if (!string.IsNullOrEmpty(secondLine))
            DrawCentered(secondLine);

        for (int i = 0; i < borderWidth; i++)
            DrawBorder();
    }
}