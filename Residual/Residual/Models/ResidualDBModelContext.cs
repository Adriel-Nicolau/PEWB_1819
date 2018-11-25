using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class ResidualDBModelContext : DbContext
    {
        public ResidualDBModelContext() : base("name=ResidualDBModelContext") { }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityType> EntitiesType { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceType> ServicesTypes { get; set; }
        

    }
}