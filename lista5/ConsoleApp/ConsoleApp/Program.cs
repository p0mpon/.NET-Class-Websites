namespace ConsoleFirst
{
    class Program
    {
        static double[] getCoefficients()
        {
            Console.WriteLine("Input coefficient for x²:");
            Console.WriteLine();
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Input coefficient for x¹:");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Input coefficient for x⁰:");
            double c = Convert.ToDouble(Console.ReadLine());

            return new double[]{a, b, c};
        }

        static (int, double[]) solveEquation(double a, double b, double c)
        {
            double[] result = new double[1];
            int numberOfSolutions;
            
            if (a == 0 && b == 0)
            {
                if (c == 0)
                {
                    return (int.MaxValue, Array.Empty<double>());
                }
                else
                {
                    return (0, Array.Empty<double>());
                }
            }
            else if (a == 0)
            {
                result[0] = -c / b;
                numberOfSolutions = 1;
            }
            else
            {
                double delta = double.Pow(b, 2) - 4 * a * c;

                if (delta < 0)
                {
                    return (0, Array.Empty<double>());
                }
                else if (delta == 0)
                {
                    result[0] = -b / (2 * a);
                    numberOfSolutions = 1;
                }
                else
                {
                    result = new double[2];
                    
                    double deltaSqrt = Math.Sqrt(delta);
                    double x1 = (-b - deltaSqrt) / (2 * a);
                    double x2 = (-b + deltaSqrt) / (2 * a);

                    result[0] = x1;
                    result[1] = x2;
                    numberOfSolutions = 2;
                }
            }

            return (numberOfSolutions, result);
        }

        static void printEquation(double a, double b, double c)
        {
            if (a == 0 && b == 0)
            {
                Console.WriteLine("Your equation:   y = " + c);
            }
            else if (a == 0)
            {
                Console.WriteLine("Your equation:   y = " + b + " * x + " + c);
            }
            else
            {
                Console.WriteLine("Your equation:   y = " + a + " * x^2 + " + b + " * x + " + c);
            }
            Console.WriteLine();
        }

        static void printSolution((int, double[]) solution)
        {
            int numberOfSolutions = solution.Item1;
            double[] values = solution.Item2;
            
            switch (numberOfSolutions)
            {
                case 0:
                    Console.Write("No solution :c");
                    break;
                case 1:
                    Console.Write("Solution:    {0}", values[0]);
                    break;
                case 2:
                    Console.Write($"Solutions:    {values[0]}    {values[1]}");
                    break;
                case int.MaxValue:
                    Console.Write("Infinite solutions");
                    break;
                default:
                    Console.Write("There has been an error");
                    break;
            }
        }
        
        static void Main(string[] args)
        {
            double[] coefficients = getCoefficients();
            printEquation(coefficients[0], coefficients[1], coefficients[2]);
            
            (int, double[]) solution = solveEquation(coefficients[0], coefficients[1], coefficients[2]);
            printSolution(solution);
        }
    }
}