using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WheelFactory.Models;

namespace WheelFactory.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly WheelContext _context;
        public OrdersService(WheelContext context)
        {
            _context = context;
        }

        public List<Orders> GetOrders()
        {
            var orders = _context.OrderDetails.ToList();
            return (orders);

        }
        public Orders GetById(int id)
        {
            return _context.OrderDetails.Find(id);
        }


        public List<Orders> GetComplete()
        {
            return _context.OrderDetails.Where(o => o.Status == "completed").ToList();
        }
        public List<Orders> GetCurrent()
        {
            return _context.OrderDetails.Where(o => o.Status != "completed").ToList();
        }

        public List<Orders> GetScraped()
        {
            return _context.OrderDetails.Where(o => o.Status == "Scrap").ToList();
        }


        public bool AddOrders(Orders order)
        {
            _context.OrderDetails.Add(order);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateOrder(int id, String status)
        {
            var order = _context.OrderDetails.Find(id);

            if (order != null)
            {
                order.Status = status;

                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ScrapOrder(int id)
        {
            var order = _context.OrderDetails.Find(id);

            if (order != null)
            {
                order.Status = "Scrap";
                _context.SaveChanges();
                return true;
            }
            return false;
        }



        public bool UpdateInventOrder(int id)
        {
            var order = _context.OrderDetails.Find(id);

            if (order != null)
            {
                order.Status = "Soldering";
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        public List<Orders> GetInventOrders()
        {
            return _context.OrderDetails
                           .Where(o => o.Status == "neworder")
                           .ToList();
        }


    }
}
