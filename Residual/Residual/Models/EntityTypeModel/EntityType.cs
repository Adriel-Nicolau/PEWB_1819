using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Residual.Models
{
    public class EntityType
    {

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    
        [Required]
        [ForeignKey("Role")]
        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
   

    }
}