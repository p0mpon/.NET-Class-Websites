using Microsoft.AspNetCore.Mvc;

namespace MVC_architecture.Controllers;

public class ToolController : Controller
{
    // GET Tool/Solve/a/b/c
    public ActionResult Solve(double a, double b, double c)
    {
        var (numberOfSolutions, values) = SolveEquation(a, b, c);

        string resultText;
        string cssClass;

        switch (numberOfSolutions)
        {
            case 0:
                resultText = "No solution :c";
                cssClass = "no-solution";
                break;
            case 1:
                resultText = $"Solution:    {values[0]}";
                cssClass = "one-solution";
                break;
            case 2:
                resultText = $"Solutions:    {values[0]}    {values[1]}";
                cssClass = "two-solutions";
                break;
            case int.MaxValue:
                resultText = "Infinite number of solutions";
                cssClass = "identity";
                break;
            default:
                resultText = "There has been an error";
                cssClass = "no-solution";
                break;
        }

        ViewBag.Result = resultText;
        ViewBag.CssClass = cssClass;

        return View();
    }

    private static (int, double[]) SolveEquation(double a, double b, double c)
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
            double delta = Math.Pow(b, 2) - 4 * a * c;

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
}