using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class ServiceEntityRelational
    {
        public int ID { get; set; }
        [Required]
        public string EntityID { get; set; }
        [Required]
        public int ServiceID { get; set; }
        [DisplayFormat(NullDisplayText = "Por inicializar")]
        public string Status { get; set; }


        public virtual Entity Entity { get; set; }
        public virtual Service Service { get; set; }
    }
}