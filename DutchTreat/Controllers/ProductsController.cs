using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IDutchRepository _repository;

        public ProductsController(ILogger<ProductsController> logger, IDutchRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType((200))]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Bad request");
            }
        }
    }

    
}