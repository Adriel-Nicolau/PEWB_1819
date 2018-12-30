using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class ServiceType
    {
        public int ID { get; set; }
        [Display(Name = "Tipo de Serviço")]
        public string Name { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}