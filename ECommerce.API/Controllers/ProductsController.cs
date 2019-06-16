using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Model;
using ECommerce.ProductCatalog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductCatalogService _catalogService;

        public ProductsController()
        {
            _catalogService = ServiceProxy.Create<IProductCatalogService>(
                new Uri("fabric:/ECommerce/ECommerce.ProductCatalog"),
                new ServicePartitionKey(0));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            IEnumerable<Product> products;
            try
            {
                products = await _catalogService.GetAllProducts();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return products;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody]Product product)
        {
            await _catalogService.AddProduct(product);
        }

    }
}
