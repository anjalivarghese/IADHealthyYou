using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyYou_V2.Models
{
    public class SendEmailViewModel
    {
      
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Contents { get; set; }

    }
}