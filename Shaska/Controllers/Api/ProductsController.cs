using Shaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using Shaska.Dto;

namespace Shaska.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private ApplicationDbContext _context;
        public ProductsController() {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetProducts(string query = null)
        {

            var productsQuery = _context.Products.Include(m => m.ProductCategory);

            if (!String.IsNullOrWhiteSpace(query))
                productsQuery = productsQuery.Where(m => m.ProductName.Contains(query));
            var productDtos = productsQuery.
            ToList().Select(Mapper.Map<Product , ProductDto>);
            return Ok(productDtos);
        }

        // GET /api/products/1
        public IHttpActionResult GetProducts(int id)
        {
            var product = _context.Products.SingleOrDefault(c => c.ProductId == id);
            if (product == null)
                return NotFound();
            return Ok(Mapper.Map<Product, ProductDto>(product));
        }

        //POST /api/product
        [HttpPost]
        public IHttpActionResult CreateProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var product = Mapper.Map<ProductDto, Product>(productDto);
            _context.Products.Add(product);
            _context.SaveChanges();

            productDto.ProductId = product.ProductId;
            return Created(new Uri(Request.RequestUri + "/" + product.ProductId), productDto);
        }

        //PUT /api/product/1
        [HttpPut]
        public IHttpActionResult UpdateProduct(int id, ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var productInDb = _context.Products.SingleOrDefault(c => c.ProductId == id);
            if (productInDb == null)
                return NotFound();
            Mapper.Map(productDto, productInDb);

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + id), productDto);
        }

        //DELETE /api/product/1
        [HttpDelete]
        
        public IHttpActionResult DeleteProduct(int id)
        {

            var product = _context.Products.SingleOrDefault(c => c.ProductId == id);
            if (product == null)
                return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }
    
    }
}
