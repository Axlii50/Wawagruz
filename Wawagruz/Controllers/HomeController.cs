using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wawagruz.Models;

namespace Wawagruz.Controllers
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
            Console.WriteLine("test");
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("Name,Email,Subject,Message")] ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                //send Mail as contact form 
                Mail.SendMail(contactModel);
            }
            return View(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult VeryficationPanelMain()
        {
            //check for expired tokens in global list and remove any
            foreach (Token x in Startup.Tokens)
                if (x.IsExpired() == true)
                {
                    Startup.Tokens.Remove(x);
                    break;
                }

            //create new token 
            var token = new Token(Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(30));
            //add token to global list 
            Startup.Tokens.Add(token);
#if (!DEBUG)
            //send token to mail
            Mail.SendMail(token.TokenString);
#endif
            return View("Veryfication_Panel_1");
        }
        [HttpPost]
        public IActionResult VeryficationPanelMain([Bind("Token")] string Token)
        {
            //check for each token present in global list of them if any is equal
            foreach (var x in Startup.Tokens)
                if (x.TokenString == Token)
                {
                    //check if token is not expired if is then remove it other wise remove token and redirect
                    if (x.IsExpired())
                    {
                        Startup.Tokens.Remove(x);
                        break;
                    }
                    else
                    {
                        Startup.Tokens.Remove(x);
                        return RedirectToAction("Index", "Panel_1_Order");
                    }
                }
#if (DEBUG)
            return RedirectToAction("Index", "Panel_1_Order");
#endif
            return View(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult VeryficationPanelDelivery()
        {
            return View("Veryfication_Panel_2_1");
        }

        //add in future tracking how many each mail loged in panel and show it in statistic panel
        [HttpPost]
        public IActionResult VeryficationPanelDelivery([Bind("Mail")] string mail)
        {
            System.Diagnostics.Debug.WriteLine("tet");
#if (!DEBUG)
            //if model is not valid for operations (mail is null) return index
            if (!ModelState.IsValid) return Index(); 
#endif
            //get all mails (lines) from file where all allowed mails for second panel are allowed
            string[] Lines = System.IO.File.ReadAllLines(FileMethods.GetPath("AllowedMails.txt"));

            //check if any mail from allowed is equal to this from model 
            foreach (string item in Lines)
            {
                if(mail == item)
                {
                    //create new token
                    var token = new Token(Guid.NewGuid().ToString(), DateTime.Now.AddMinutes(30));
                    //add token to global list 
                    Startup.Tokens.Add(token);
                    System.Diagnostics.Debug.WriteLine(token.TokenString);
#if (!DEBUG)
                    //send mail with token to equal mail from Allowed list
                    Mail.SendMail(token.TokenString);
#endif
                    return View(nameof(Veryfication_Panel_2_2));
                }
            }
#if (DEBUG)
            return View(nameof(Veryfication_Panel_2_2));
#endif
            return View(nameof(Index));
        }

        [HttpGet]
        [NoDirectAccess]
        public IActionResult Veryfication_Panel_2_2()
        {
            return View();
        }

        [HttpPost]
        [NoDirectAccess]
        public IActionResult Veryfication_Panel_2_2([Bind("Token")] string Token)
        {
            //check for each token present in global list of them if any is equal
            foreach (var x in Startup.Tokens)
                if (x.TokenString == Token)
                {
                    //check if token is not expired if is then remove it other wise remove token and redirect
                    if (x.IsExpired())
                    {
                        Startup.Tokens.Remove(x);
                        break;
                    }
                    else
                    {
                        Startup.Tokens.Remove(x);
                        return RedirectToAction("Index", "Panel_2_Order");
                    }
                }
#if (DEBUG)
            //easier access to panel 
            return RedirectToAction("Index", "Panel_2_Order");
#endif
            return View(nameof(Index));
        }
    }
}
