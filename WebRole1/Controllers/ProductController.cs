using WebRole1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;



namespace WebRole1.Controllers
{
   
    public class ProductController : ApiController
    {
        Product[] products = new Product[] {
            new Product { Id = 1, Name = "Tomato Soup",Category = "Groceries",Price = 1 },
            new Product { Id = 2, Name = "Yo-yo",Category = "Toys",Price = 3.75M },
            new Product { Id = 3, Name = "Hammer",Category = "Hardware",Price = 16.99M },
        };

        [HttpGet]
        [Route("api/v1/products")]
        //http://localhost:51543/api/v1/products
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        [HttpGet]
        [Route("api/v1/products/{id:int:min(2)}")]
        //http://localhost:51543/api/v1/products/3
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }

        }

    }
}
