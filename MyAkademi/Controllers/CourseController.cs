using Microsoft.AspNetCore.Mvc;
using MyAkademi.Models;

namespace MyAkademi.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            var candidates = Repository.Applications;
            return View(candidates);
        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply([FromForm]Candidate candidate)
        {
            if(Repository.Applications.Any(a => a.Email.Equals(candidate.Email)))
            {
                ModelState.AddModelError("", "There is already application for you.");
            }
            if (ModelState.IsValid)
            {
                Repository.Add(candidate);
                return View("Feedback", candidate);
            }
            return View();
        }
    }
}
