using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class Equipment
    {
        public int ID { get; set; }
        [Required]
        [ForeignKey("EquipmentType")]
        [Display(Name = "Tipo")]
        public int EquipmentTypeID { get; set; }

        [Required]
        [ForeignKey("EquipmentState")]
        [Display(Name = "Estado")]
        public int EquipmentStateID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("ServiceType")]
        [Display(Name = "Tipo de Serviço")]
        public int ServiceTypeID { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        public virtual EquipmentType EquipmentType { get; set; }
        public virtual EquipmentState EquipmentState { get; set; }
    }
}