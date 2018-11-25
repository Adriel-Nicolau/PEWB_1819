using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class Action
    {
        public int ID { get; set; }
        public int Name { get; set; }
        [Required]
        [ForeignKey("Role")]
        public int RoleID  { get; set; }
        public virtual Role Role { get; set; }
    }
}