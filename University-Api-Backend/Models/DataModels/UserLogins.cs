﻿using System.ComponentModel.DataAnnotations;

namespace University_Api_Backend.Models.DataModels
{
    public class UserLogins
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
