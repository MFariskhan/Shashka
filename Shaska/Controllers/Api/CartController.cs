using Shaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Shaska.Dto;
using AutoMapper;

namespace Shaska.Controllers.Api
{
    public class CartController : ApiController
    {
           private ApplicationDbContext _context;
        public CartController() {
            _context = new ApplicationDbContext();
        }
         [HttpGet]
        public IHttpActionResult GetCarts(string query = null)
        {
            var memberId = User.Identity.GetUserId().ToString();

            var cartQuery = _context.Carts.Include(c => c.product).Where(c => c.UserId == memberId);

            //var productsQuery = _context.Products.Include(m => m.ProductCategory);

            if (!String.IsNullOrWhiteSpace(query))
                cartQuery = cartQuery.Where(m => m.product.ProductName.Contains(query));
            var cartDtos = cartQuery.
            ToList().Select(Mapper.Map<Cart, CartDto>);
            return Ok(cartDtos);
        }
        //
        [HttpDelete]
        public IHttpActionResult DeleteCart(int id)
        {
            var cart = _context.Carts.SingleOrDefault(m=> m.Id==id);
            if (cart == null)
                return NotFound();
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return Ok();
        }
    }
}
