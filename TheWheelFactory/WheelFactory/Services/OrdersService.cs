using System.Collections.Generic;
using WheelFactory.Models;

namespace WheelFactory.Services
{
    public class OrdersService
    {
        private readonly WheelContext _context;
        public OrdersService(WheelContext context)
        {
            _context = context;
        }
        public List<Orders> GetOrders()
        {
            return _context.Order.ToList();
        }
        public Orders GetById(int id)
        {
            var orders =_context.Order.Find(id);
            return (orders);

        }
        public bool AddOrders(OrderDTO order)
        {
            Orders obj = new Orders();
            obj.ClientName = order.ClientName;
            obj.Notes = order.Notes;
            obj.Status = order.Status;
            obj.Year = order.Year;
            obj.Make = order.Make;
            obj.Model = order.Model;
            obj.DamageType = order.DamageType;
            obj.ImageUrl = order.ImageUrl;
            obj.CreatedAt = order.CreatedAt;
            _context.Order.Add(obj);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateOrder(int id, OrderDTO orderDto)
        {
            var order = _context.Order.Find(id);

            if (order != null)
            {

                order.ClientName = orderDto.ClientName;
                order.Notes = orderDto.Notes;
                order.Status = orderDto.Status;
                order.Year = orderDto.Year;
                order.Make = orderDto.Make;
                order.Model = orderDto.Model;
                order.DamageType = orderDto.DamageType;
                order.ImageUrl = orderDto.ImageUrl;
                _context.SaveChanges();

                return true;
            }

            return false; 
        }

        public bool DeleteOrder(int id)
        {
            var order = _context.Order.Find(id);
            if (order != null)
            {
                _context.Order.Remove(order);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


    }
}
