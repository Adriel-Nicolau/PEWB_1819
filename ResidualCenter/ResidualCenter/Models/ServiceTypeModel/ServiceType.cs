﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class ServiceType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}