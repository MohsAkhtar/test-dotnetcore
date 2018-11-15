using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;

        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // called when making get request
        [HttpGet("contact")] // lets contact be more discoverable by being /contact not /app/contact
        public IActionResult Contact()
        {
            return View();
        }

        // called when posting (object model) is payload
        // ContactViewModel model is used for model binding of the data from contact form
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            // checks if rules in ContactViewModel have been followed
            if (ModelState.IsValid)
            {
                // send the email
                _mailService.SendMessage("mohs.akhtar@gmail.com",
                    model.Subject,
                    $"From:{model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear(); // clears form when message is sent
            }

            return View();
        }

            public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();
            return View(results.ToList());
        }
    }
}
