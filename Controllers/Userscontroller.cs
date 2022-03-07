using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Userscontroller : ControllerBase
    {
        private readonly DataContext context;
        public Userscontroller(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUsers>>> getAllUsers()
        {
            return await context.Users.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AppUsers>> getUser(int id)
        {

            return await context.Users.FindAsync(id);
        }


    }
}