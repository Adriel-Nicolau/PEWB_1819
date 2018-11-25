using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class EquipmentType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}