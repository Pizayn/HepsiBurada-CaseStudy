﻿using Catalog.API.Entities;
using Catalog.API.GrpcServices;
using Catalog.API.Repositories;
using Discount.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly ITimeRepository _timeRepository;

        public CatalogController(IProductRepository repository, ITimeRepository timeRepository, DiscountGrpcService discountGrpcService, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _timeRepository = timeRepository ?? throw new ArgumentNullException(nameof(timeRepository)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("[action]/{productCode}", Name = "GetProductByProductCode")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByProductCode(string productCode)
        {
            var time =await _timeRepository.GetTime();
            var hour = time.Hour == 0 ? 1 : time.Hour;
            var product = await _repository.GetProductByProductCode(productCode);
            var campaign = await _discountGrpcService.GetCampaign(productCode);


            if (product == null)
            {
                _logger.LogError($"Product with code: {productCode}, not found.");
                return NotFound();
            }
            if(!string.IsNullOrEmpty(campaign.ProductCode))
            {
    
                var descentRate = ((double)campaign.PriceManipulationLimit / (double)campaign.Duration) * hour;
                product.Price -=  product.Price * descentRate /100;

            }
            return Ok(product);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            var existProduct = await _repository.GetProductByProductCode(product.ProductCode);
            if(existProduct != null)
            {
                _logger.LogError($"You can not create same productCode: {product.ProductCode}.");
                return BadRequest();

            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
         
            await _repository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var existProduct = await _repository.GetProductByProductCode(product.ProductCode);
            existProduct.Stock = product.Stock;
            return Ok(await _repository.UpdateProduct(existProduct));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.DeleteProduct(id));
        }


    }
}
