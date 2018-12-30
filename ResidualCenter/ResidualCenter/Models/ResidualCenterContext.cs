using System;
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

        public virtual DbSet<ServiceType> ServicesTypes { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }
        public virtual DbSet<EquipmentState> EquipmentStates { get; set; }
        public virtual DbSet<ServiceRequest> ServiceRequests { get; set; }
        public virtual DbSet<ResidueType> ResidueTypes { get; set; }

        public virtual DbSet<ServiceRequestStatus> ServiceRequestStatus { get; set; }

    }
}