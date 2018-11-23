using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class ResidualDBContext : DbContext
    {
        public ResidualDBContext() : base("name=ResidualDBContext") { }
                public virtual DbSet<Entity> Entities { get; set; }
    }
}