using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOS;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class Accountcontroller : BaseApicontroller
    {
        private readonly DataContext context;
        private readonly ITokenServices tokenServices;

        public Accountcontroller(DataContext _context ,ITokenServices tokenServices)
        {

            context = _context;
            this.tokenServices = tokenServices;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> register(RegisterDTO registerDTO)
        {

            if (await UserExist(registerDTO.Username)) return BadRequest("User is already Exist");



            var HMAC = new HMACSHA512();

            AppUsers user = new AppUsers
            {
                UserName = registerDTO.Username.ToLower(),
                Password = HMAC.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)),
                SaltPassword = HMAC.Key
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return new  UserDTO{
                    UserName =user.UserName,
                    Token = tokenServices.CreateToken(user)
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult <UserDTO> >login( LoginDTO loginDTO){

            var userEntity =  await context.Users.SingleOrDefaultAsync(x => x.UserName== loginDTO.Username.ToLower());

            if(userEntity == null) return Unauthorized("Invalid UserName"); 

            using var hmach = new HMACSHA512(userEntity.SaltPassword);

           var ComputeHash = hmach.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != userEntity.Password[i])
                 return Unauthorized("Invaled Password"); 
            }

             return new  UserDTO{
                    UserName =userEntity.UserName,
                    Token = tokenServices.CreateToken(userEntity)
            };
        }
 

        private async Task<bool> UserExist(string UserName)
        {

            return await context.Users.AnyAsync(x => x.UserName == UserName.ToLower());

        }


    }
}