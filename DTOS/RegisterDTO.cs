using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOS
{
    public class RegisterDTO
    {
        [Required]
        public String Username { get; set; }

        [Required]
        public string password { get; set; }
    }
}