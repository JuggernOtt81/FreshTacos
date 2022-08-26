using FreshTacos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FreshTacos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reverse()
        {
            Palindrome model = new();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reverse(Palindrome palindrome)
        {
            string inputWord = palindrome.InputWord; //lowercase i for local variable
            string revWord = "";


            for (int i = inputWord.Length - 1; i >= 0; i--) //start at the end
            {
                revWord += inputWord[i]; //concatenate letter from inputWord to revWord

            }

            palindrome.RevWord = revWord;


            revWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9}]+", "");
            inputWord = Regex.Replace(inputWord.ToLower(), "[^a-zA-Z0-9}]+", "");

            if (revWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $"{palindrome.InputWord} reversed is {palindrome.RevWord}: Palindrome Detected!";
            }
            else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $"{palindrome.InputWord} reversed is {palindrome.RevWord}: No palindromes found.";
            }

            return View(palindrome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
