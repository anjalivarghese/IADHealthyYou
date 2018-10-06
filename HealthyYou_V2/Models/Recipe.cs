using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthyYou_V2.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public string RecipeName { get; set; }
        public Decimal Calper100gram { get; set; }
        public List<Planner> Planners { get; set; }
    }
}