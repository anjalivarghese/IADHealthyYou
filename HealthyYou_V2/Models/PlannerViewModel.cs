using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthyYou_V2.Models
{
    public class PlannerViewModel
    {
       
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public Decimal Weight { get; set; }
        public DateTime OnDate { get; set; }
    }
}