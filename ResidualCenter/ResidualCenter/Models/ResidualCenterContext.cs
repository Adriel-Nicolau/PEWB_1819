﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class ResidualCenterContext : ApplicationDbContext
    {
        public ResidualCenterContext()
        {

        }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceType> ServicesTypes { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }

    }
}