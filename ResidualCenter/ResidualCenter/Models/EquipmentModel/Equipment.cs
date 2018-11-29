using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class Equipment
    {
        public int ID { get; set; }
        [Required]
        public int EquipmentTypeID { get; set; }
        [Required]
        public string Name { get; set; }

        public bool Using { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public virtual EquipmentType EquipmentType { get; set; }
    }
}