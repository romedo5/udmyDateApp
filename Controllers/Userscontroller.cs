using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    
    
    public class Userscontroller : BaseApicontroller
    {


        private readonly DataContext context;
        public Userscontroller(DataContext context)
        {
            this.context = context;
        }
 
        [HttpGet]
        [AllowAnonymous] 
        public async Task<ActionResult<IEnumerable<AppUsers>>> getAllUsers()
        {
            return await context.Users.ToListAsync();
        }
       
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<AppUsers>> getUser(int id)
        {

            return await context.Users.FindAsync(id);
        }


    }
}