using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class Entity
    {
        
        public int ID { get; set; }

        public int IDentityType { get; set; } // FK from entityTypeModel

        
        [Display(Name = "Nome Completo")]
        public string Name { get; set; }

        [Display(Name = "Contacto")]
        public int Contact { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Genero")]
        public string Gender { get; set; }

        public int IDlocation { get; set; } // FK from locationModel
        [Display(Name = "Endereço")]
        public string Adress { get; set; }
    }

}
