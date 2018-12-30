using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ResidualCenter.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    public class RegisterEmployeeViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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
        [Display(Name = "Região")]
        public int LocationID { get; set; } // FK from locationModel


    }
    public class AddEquipmentViewModel
    {

        [Required]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "Nome não pode ter mais que 50 caracteres")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public int EquipmentTypeID { get; set; } // FK from locationModel
        [Required]
        [Display(Name = "Estado")]
        public int EquipmentStateID { get; set; } // FK from locationModel

        [Required]
        [Display(Name = "Tipo de Serviço")]
        public int ServiceTypeID { get; set; } // FK from locationModel
    }
    public class ChangeStatusServiceResquestViewModel
    {
        public int ID { get; set; }
        public int ServiceRequestStatusID { get; set; }
        //public int EmployeeListID { get; set; }
    }
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}