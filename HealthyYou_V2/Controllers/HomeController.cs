﻿using HealthyYou_V2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthyYou_V2.Controllers
{
    [Authorize(Roles = "Customer")]
    public class HomeController : Controller
    {

        ApplicationDbContext context;

        public HomeController()
        {

            context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult CannotAddtoPlanner()
        {

            return View();
        }


        [Authorize]
        public ActionResult ViewGyms()
        {
            return View(context.Gym.ToList());
        }

        public ActionResult ViewRecipes()
        {
            return View(context.Recipes.ToList());
        }

        [HttpGet]
        public ActionResult MakePlanner(string id)
        {
            int a = Convert.ToInt32(id);
            var recipe = context.Recipes.FirstOrDefault(d => d.ID == a);
            var planner = new PlannerViewModel();
            planner.RecipeID = recipe.ID;
            planner.RecipeName = recipe.RecipeName;

            return View(planner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakePlanner([Bind(Include = "ID,RecipeID,OnDate,Weight")]PlannerViewModel plannerViewModel)
        {
            var customerId = GetCustomerId();
            var customerApps = context.Planner.Where(a => a.CustomerID == customerId).ToList();
            foreach (var app in customerApps)
            {
                if (app.OnDate == plannerViewModel.OnDate && app.RecipeID == plannerViewModel.RecipeID)
                {
                    return RedirectToAction("CannotAddtoPlanner");
                }
            }
            var planner = new Planner()
            {
                CustomerID = customerId,
                RecipeID = plannerViewModel.RecipeID,
                OnDate = plannerViewModel.OnDate,
                Weight = plannerViewModel.Weight,
                Calconsumed = plannerViewModel.Weight * context.Recipes.FirstOrDefault(a => a.ID == plannerViewModel.RecipeID).Calper100gram,
            };

            context.Planner.Add(planner);
            context.SaveChanges();

            return RedirectToAction("ViewRecipes");
        }

        public ActionResult Planner()
            {
            int customerId = GetCustomerId();
            decimal total = 0; 
            var plan = context.Planner.Where(a => a.CustomerID == customerId).ToList();
            foreach (var entry in plan)
            {
                total = total + entry.Calconsumed;
            }
            ViewBag.total = total;
            return View(context.Planner.Include("Recipe").Where(a => a.CustomerID == customerId).ToList());               
            }

        private int GetCustomerId()
        {
            var customerAuthId = User.Identity.GetUserId();
            var customerEmail = context.Users.FirstOrDefault(u => u.Id == customerAuthId).Email;
            return context.Customers.FirstOrDefault(c => c.Email == customerEmail).ID;
        }

        public ActionResult Details()
        {
            int id = GetCustomerId();
            Customer customer = context.Customers.Find(id);
            decimal H = (customer.Height) / 100;
            decimal BMI = Math.Round(customer.Weight / (H*H));
            ViewBag.BMI = BMI;
            return View(customer);
        }

        public ActionResult WriteReview(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriteReview(Review review, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(new { Id = id });
            }


            review.GymID = id;
            review.CustomerID = GetCustomerId();
            review.OnDate = DateTime.Now;
            review.IsArchive = false;
            context.Reviews.Add(review);
            context.SaveChanges();
            return RedirectToAction("ViewGyms");
        }

        [HttpGet]
        public ActionResult ViewReview(int id)
        {
            return View(context.Reviews.Include("Gym").Include("Customer").Where(r => r.GymID == id).ToList());
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
       
    }
}