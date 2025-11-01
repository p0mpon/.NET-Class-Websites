namespace ConsoleFirst
{
    class Program
    {
        static double[] getCoefficients()
        {
            Console.WriteLine("Input coefficient for x²:");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Input coefficient for x¹:");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Input coefficient for x⁰:");
            double c = Convert.ToDouble(Console.ReadLine());

            return new double[]{a, b, c};
        }

        static double[] solveEquation(double a, double b, double c)
        {
            double[] result = new double[1];
            
            if (a == 0 && b == 0)
            {
                if (c == 0)
                {
                    result[0] = Double.PositiveInfinity;
                }
                else
                {
                    return Array.Empty<double>();
                }
            }
            else if (a == 0)
            {
                result[0] = -c / b;
            }
            else
            {
                double delta = double.Pow(b, 2) - 4 * a * c;

                if (delta < 0)
                {
                    return Array.Empty<double>();
                }
                else if (delta == 0)
                {
                    result[0] = -b / (2 * a);
                }
                else
                {
                    result = new double[2];
                    
                    double deltaSqrt = Math.Sqrt(delta);
                    double x1 = (-b - deltaSqrt) / (2 * a);
                    double x2 = (-b + deltaSqrt) / (2 * a);

                    result[0] = x1;
                    result[1] = x2;
                }
            }

            return result;
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

        static void printSolution(double[] solution)
        {
            int length = solution.Length;
            switch (length)
            {
                case 0:
                    Console.Write("No solution :c");
                    break;
                case 1:
                    if (Double.IsPositiveInfinity(solution[0]))
                    {
                        Console.Write("Infinite solutions");
                    }
                    else
                    {
                        Console.Write("Solution:    {0}", solution[0]);
                    }
                    break;
                case 2:
                    Console.Write($"Solutions:    {solution[0]}    {solution[1]}");
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
            
            double[] solution = solveEquation(coefficients[0], coefficients[1], coefficients[2]);
            printSolution(solution);
        }
    }
}