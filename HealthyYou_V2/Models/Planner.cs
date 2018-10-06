using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthyYou_V2.Models
{
    public class Planner
    {
        public int ID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Recipe")]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public DateTime OnDate { get; set; }
        public Decimal Weight { get; set; }
        public Decimal Calconsumed { get; set; }
    }
}