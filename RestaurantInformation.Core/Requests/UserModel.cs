using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantInformation.Core.Requests
{
    public class UserModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Maximum length reached")]
        public string Username { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximum length reached")]
        public string Password { get; set; }
    }
}
