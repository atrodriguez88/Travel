using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebTravel.ViewModels
{
    public class ControlViewModel
    {
        public string id { get; set; }
        [StringLength(20)]
        [Required]
        public string username { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public string Rol { get; set; }
    }
}