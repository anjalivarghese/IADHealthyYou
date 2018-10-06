using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthyYou_V2.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Decimal Weight { get; set; }
        public Decimal Height { get; set; }
        public Decimal Age { get; set; }
        public Boolean IsActive { get; set; }
        public List<Planner> Planners { get; set; }
        public List<Gym> Gyms { get; set; }
    }
}
    