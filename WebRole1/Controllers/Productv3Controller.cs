
using WebRole1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebRole1.Controllers
{
    public class Productv3Controller : ApiController
    {

        //ties controller to repository
        static readonly ProductRepository repository = new ProductRepository();

        [Authorize]
        [HttpGet]
        [Route("api/v3/products")]
        //https://localhost:44364/api/v3/products
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        [Authorize]
        [HttpGet]
        [Route("api/v3/products/{id:int:min(2)}", Name = "getProductByIdv3")]
        //https://localhost:44364/api/v3/products/3
        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [Authorize]
        [HttpGet]
        [Route("api/v3/products", Name = "GetProductsByCategoryv3")]
        //https://localhost:44364/api/v3/products?category=Groceries

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        [Authorize]
        [HttpPost]
        [Route("api/v3/products")]
        //Header { Content-Type = "application/json"   }
        //body 
        //{
        //	"name":"Testing",
        //	"Category":"Toys",
        //	"Price":"37"
        //}
        public HttpResponseMessage PostProduct(Product item)
        {
            if (ModelState.IsValid)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
                //getProductByID is the title for the
                string uri = Url.Link("getProductByIdv3", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/v3/products/{id:int}")]
        //https://localhost:44364/api/v3/products/1
        public HttpResponseMessage UpdateProduct(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = id;
                if (!repository.Update(product))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                    

                }
                else
                {
                    var response = Request.CreateResponse<Product>(HttpStatusCode.Created, product);
                    //getProductByID is the title for the
                    string uri = Url.Link("getProductByIdv3", new { id = product.Id });
                    response.Headers.Location = new Uri(uri);
                    return response;
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

          [Authorize]
        [HttpDelete]
        [Route("api/v3/products/{id:int}")]
        //https://localhost:44364/api/v3/products/1
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
