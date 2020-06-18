using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using projeto.Data;
using projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projeto.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("api/produtos")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] ApplicationDbContext context)
        {
            var products = await context.Products.ToListAsync();
            return products;
        }

        [HttpPost]
        [Route("api/produtos")]
        public async Task<ActionResult<Product>> Post(
            [FromServices] ApplicationDbContext context,
            [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(model);
            }
        }

        [HttpGet]
        [Route("api/produto/{id}")]
        public async Task<ActionResult<Product>> GetById(
            [FromServices] ApplicationDbContext context, int id)
        {
            var product = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpDelete]
        [Route("api/produto/{id}")]
        public async Task<ActionResult<Product>> DeleteConfirmed(
            [FromServices] ApplicationDbContext context, int id)
        {
            var product = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return product;
        }

        [HttpPut]
        [Route("api/produto/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(
            [FromServices] ApplicationDbContext context, 
            int id,
            [FromBody] Product produto)
        {
            var product = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Nome = produto.Nome;
            product.Descricao = produto.Descricao;
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
