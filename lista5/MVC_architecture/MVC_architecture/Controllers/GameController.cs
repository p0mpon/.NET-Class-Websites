using Microsoft.AspNetCore.Mvc;

namespace MVC_architecture.Controllers;

public class GameController : Controller
{
    // zmienne statyczne
    private static int _rangeN = 10;
    private static int _randomValue = -1;
    private static int _attemptsCount; 
    private static Random _rng = new Random();

    // /Game/Set/n
    public IActionResult Set(int n)
    {
        Console.WriteLine(n);
        _rangeN = n;
        _randomValue = -1;
        _attemptsCount = 0;

        ViewBag.Message = $"Range set to 0 to {_rangeN - 1}. Draw a number to guess!";
        ViewBag.CssClass = "set-info";

        return View();
    }


    // /Game/Draw
    public IActionResult Draw()
    {
        _randomValue = _rng.Next(0, _rangeN);
        _attemptsCount = 0;

        ViewBag.Message = $"Number drawed from 0 to {_rangeN - 1}. Now take a guess!";
        ViewBag.CssClass = "draw-info";

        return View();
    }

    // /Game/Guess/x
    public IActionResult Guess(int guess)
    {
        if (_randomValue == -1)
        {
            ViewBag.Message = "Use /Game/Draw to draw a random number first :)";
            ViewBag.CssClass = "error-info";
            return View();
        }

        _attemptsCount++;

        if (guess < _randomValue)
        {
            ViewBag.Message = $"Attempt #{_attemptsCount}: {guess}? seems to small.";
            ViewBag.CssClass = "too-small";
        }
        else if (guess > _randomValue)
        {
            ViewBag.Message = $"Attempt #{_attemptsCount}: {guess}? now that's too big.";
            ViewBag.CssClass = "too-big";
        }
        else
        {
            ViewBag.Message = $"CONGRATS! You guessed number {_randomValue} in {_attemptsCount} attempts!";
            ViewBag.CssClass = "correct";

            _randomValue = -1;
            _attemptsCount = 0;
        }

        return View();
    }
}