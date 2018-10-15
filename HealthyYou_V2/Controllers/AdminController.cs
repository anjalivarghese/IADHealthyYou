using HealthyYou_V2.Models;
using Microsoft.AspNet.Identity;
using HealthyYou_V2.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HealthyYou_V2.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationDbContext context;
        private ApplicationUserManager _userManager;


        public AdminController()
        {
            context = new ApplicationDbContext();
        }
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewCustomer()
        {
            return View(context.Customers.ToList());
        }

        public ActionResult Suspend(int id)
        {
            var customer = context.Customers.FirstOrDefault(c => c.ID == id);
            if (customer.IsActive)
            {
                customer.IsActive = false;
            }
            else
            {
                customer.IsActive = true;
            }
            context.SaveChanges();

            TempData["customer"] = customer;
            return RedirectToAction("Send_Email",customer);
        }

        public ActionResult ViewReviews()
        {
            return View(context.Reviews.Include("Customer").Include("Gym").ToList());
        }

        public ActionResult Archive(int id)
        {
            var review = context.Reviews.FirstOrDefault(r => r.ID == id);
            if (review.IsArchive)
            {
                review.IsArchive = false;
            }
            else
            {
                review.IsArchive = true;
            }
            context.SaveChanges();
            return RedirectToAction("ViewReviews");
        }

        public ActionResult Send_Email(Customer customer)
        {
            SendEmailViewModel model = new SendEmailViewModel();
            ViewBag.toEmail = customer.Email;
            if (customer.IsActive)
            {
                ViewBag.Subject = "Healthy You Alert";
                ViewBag.Contents = "You have been activated";
            }
            else
            {
                ViewBag.Subject = "Healthy You Alert";
                ViewBag.Contents = "You have been deactivated";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return RedirectToAction("ViewCustomer");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

    }
}