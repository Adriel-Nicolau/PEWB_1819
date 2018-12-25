using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class Entity
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nome Completo")]
        [StringLength(50, ErrorMessage = "Nome não pode ter mais que 50 caracteres")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Contacto")]
        public int Contact { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Genero")]
        //vai ser um picker com as opçoes 
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Endereço")]
        public string Adress { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }


        [Required]
        [ForeignKey("Location")]
        [Display(Name = "Região")]
        public int LocationID { get; set; } // FK from locationModel


        public virtual Location Location { get; set; }
        public virtual ICollection<ServiceRequest> ServiceRequest { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}