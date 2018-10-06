﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthyYou_V2.Models
{
    public class Review
    {
        public int ID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Gym")]
        public int GymID { get; set; }
        public Gym Gym { get; set; }
        [Range(1, 10, ErrorMessage = "Rating Must be 1 to 10")]
        public int Rating { get; set; }
        [AllowHtml]
        public string ReviewText { get; set; }
        public DateTime OnDate { get; set; }
        public bool IsArchive { get; set; }
    }
}