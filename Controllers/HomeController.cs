using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]        
        public async Task<ActionResult<dynamic>> Get([FromServices]DataContext context)
        {
            var employee = new User { Id=1, UserName = "robin", Password = "123456", Role = "employee"};
            var manager = new User { Id=1, UserName = "batman", Password = "123456", Role = "manager"};
            var category = new Category { Id = 1, Title = "Informática"};
            var product = new Product { Id = 1, Category = category, Title = "moser", Price = 100, Description = "Mouser gamer"};
            context.Add(employee);
            context.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"                
            });
        }
    }
}