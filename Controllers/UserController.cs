using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Shop.Services;
using System.Collections.Generic;

namespace Shop.Controllers
{
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles="manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices]DataContext context)
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();
            return users;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        //[Authorize(Roles="manager")]
        public async Task<ActionResult<User>> Post(
            [FromServices] DataContext context,
            [FromBody]User model
        )
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {             
                //Força o usuário a ser sempre "funcionário"   
                model.Role = "employee";

                context.Users.Add(model);
                await context.SaveChangesAsync();

                //Esconde a senha
                model.Password = "";
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Não foi possível criar o usuário"});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [AllowAnonymous]
        [Authorize(Roles="manager")]
        public async Task<ActionResult<User>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody]User model
        )
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            if(id != model.Id)
                return NotFound(new {message = "Usuário não encontrado"});
            
            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Não foi possível atualizar o usuário"});
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody]User model
        )
        {
            var user = await context.Users
                .AsNoTracking()
                .Where(x => x.UserName == model.UserName && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if(user == null)
                return NotFound(new {messsage = "USuário ou senha inválidos"});
            
            var token = TokenService.GenerateToken(user);
            //Esconde a senha
            model.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }
    }
}