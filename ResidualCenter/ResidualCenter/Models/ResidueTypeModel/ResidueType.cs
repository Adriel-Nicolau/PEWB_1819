using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class ResidueType
    {
        public int ID { get; set; }
        [Display(Name = "Resíduo")]
        public string Name { get; set; }
        [Display(Name = "Unidade")]
        public string Unit { get; set; }
    }
}