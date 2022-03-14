using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte[] Password { get; set; }
        public byte[] SaltPassword { get; set; }
    }
}