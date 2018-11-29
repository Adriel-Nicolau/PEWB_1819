using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class Service
    {
        public int ID { get; set; }
        [Required]
        [ForeignKey("ServiceType")]
        public int ServiceTypeID { get; set; }

        public string Description { get; set; }

        public virtual ServiceType ServiceType { get; set; }
        public virtual ICollection<ServiceEntityRelational> ServiceEntities { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}