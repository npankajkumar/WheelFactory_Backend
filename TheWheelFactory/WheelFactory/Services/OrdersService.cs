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
            var orders =_context.OrderDetails.ToList();
            return (orders);

        }
        public Orders GetById(int id)
        {
            var orders = _context.OrderDetails.Find(id);
            return (orders);
        }


        public List<Orders> GetComplete()
        {
            return _context.OrderDetails.Where(o => o.Status == "completed").ToList();
        }
        public List<Orders> GetCurrent()
        {
            return _context.OrderDetails.Where(o => o.Status != "completed").ToList();
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
            _context.OrderDetails.Add(obj);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateOrder(int id, OrderDTO orderDto)
        {
            var order = _context.OrderDetails.Find(id);

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

        public bool UpdateInventOrder(int id,OrderDTO value)
        {
            var order = _context.OrderDetails.Find(id);

            if (order != null)
            {
                order.Status = value.Status;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        
        public List<Orders> GetInventOrders()
        {
            return _context.OrderDetails.Where(o=>o.Status=="neworder").ToList();
        }

        

    }
}
