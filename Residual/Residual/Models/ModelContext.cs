using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Residual.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ModelContext : DbContext
    {
        public ModelContext()  : base("name=ModelContext") { }

                public virtual DbSet<Teste> testes { get; set; }
   
    }

}