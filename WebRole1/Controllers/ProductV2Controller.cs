using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebRole1.Models;

namespace WebRole1.Controllers
{
    public class ProductV2Controller : ApiController
    {
        //ties controller to repository
        static readonly ProductRepository repository = new ProductRepository();

        [HttpGet]
        [Route("api/v2/products")]
        //http://localhost:51543/api/v2/products
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }


        [HttpGet]
        [Route("api/v2/products/{id:int:min(2)}", Name = "getProductById")]
        //http://localhost:51543/api/v2/products/3
        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        [HttpGet]
        [Route("api/v2/products", Name = "GetProductsByCategory")]
        //http://localhost:51543/api/v2/products?category="Groceries"
        ///product?category=Groceries
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }


        [HttpPost]
        [Route("api/v2/products")]
        //Header { Content-Type = "application/json"   }
        //body 
        //{
        //	"name":"Testing",
        //	"Category":"Toys",
        //	"Price":"375"
        //}
    public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
            //getProductByID is the title for the
            string uri = Url.Link("getProductById", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }


        [HttpPut]
        [Route("api/v2/products/{id:int}")]
        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
        }

        [HttpDelete]
        [Route("api/v2/products/{id:int}")]
        public void DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                repository.Remove(id);
            }
        }



    }
}
