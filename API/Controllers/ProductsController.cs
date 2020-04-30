using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public string GetProducts()
        {
            return "this will be a list of product";
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "this will be a list of product";
        }
    }
}
