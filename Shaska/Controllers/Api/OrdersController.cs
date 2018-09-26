using Shaska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Shaska.Dto;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Shaska.Controllers.Api
{
    public class OrdersController : ApiController
    {
        private ApplicationDbContext _context;
        public OrdersController() {
            _context = new ApplicationDbContext();
        }
        //
        [Authorize(Roles = "CanManageApplication,Rider")]
        //[Authorize(Roles = RoleName.Rider)]
        [HttpGet]
        public IHttpActionResult GetOrders(string query = null)
        {
            if (User.IsInRole("CanManageApplication"))
            {
                var ordersQuery = _context.Orders.Include(m => m.Product).Include(m => m.ApplicationUser);

                if (!String.IsNullOrWhiteSpace(query))
                    ordersQuery = ordersQuery.Where(m => m.PhoneNumber.Contains(query));
                var ordersDtos = ordersQuery.
                ToList().Select(Mapper.Map<Order, OrderDto>);
                return Ok(ordersDtos);
            }

            var RiderId = User.Identity.GetUserId();
            var ordersForRider = _context.Orders.Include(m => m.ApplicationUser).
                Include(m=> m.OrderConfirmBy).Include(m=> m.OrderAssignTo).
                Where(m=> m.OrderAssignToId== RiderId);

            if (!String.IsNullOrWhiteSpace(query))
                ordersForRider = ordersForRider.Where(m => m.PhoneNumber.Contains(query));
            var ordersForRiderDtos = ordersForRider.
                ToList().Select(Mapper.Map<Order, OrderDto>);
            return Ok(ordersForRiderDtos);
        }
        //GetOrder/1
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetOrder(string Id)
        {
            var orders = _context.Orders.Include(m=> m.ApplicationUser).Include(m=> m.Product).Where(m=> m.ApplicationUserId==Id);

            var ordersDtos = orders.
           ToList().Select(Mapper.Map<Order, OrderDto>);
            return Ok(ordersDtos);
        }
        //delete
       [Authorize(Roles = RoleName.CanManageApplication)]
       [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {

            var order = _context.Orders.SingleOrDefault(c => c.Id== id);
            if (order == null)
                return NotFound();
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok();
        }
        //
        

    }
}
