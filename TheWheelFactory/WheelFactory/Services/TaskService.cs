using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WheelFactory.Models;
using static NuGet.Packaging.PackagingConstants;
using Task = WheelFactory.Models.Task;

namespace WheelFactory.Services
{
    public class TaskService
    {
        private readonly WheelContext _context;
        private readonly OrdersService _orders;

        public TaskService(WheelContext context)
        {
            _context = context;
            _orders = new OrdersService(context);

        }
        [HttpGet]
        public List<Task> GetTask()
        {
            return _context.Tasks.ToList();
        }
        public List<Task> GetTaskById(int id)
        {
            var task = _context.Tasks.Where(t => t.OrderId == id).ToList();
            return task;

        }
        public List<Orders> GetSold()
        {
            var orders = _context.OrderDetails.Where(o => o.Status == "Soldering" || o.Status=="Redo").ToList();
            return orders;
        }
        public List<Task> GetSoldId(int id)
        {
            return _context.Tasks.Where(b => b.OrderId == id && b.Status=="soldering").ToList();
        }

        public List<Task> GetPaintId(int id)
        {
            return _context.Tasks.Where(b => b.OrderId == id && b.Status=="painting").ToList();
        }

        public bool AddSandOrders(SandDTO sand)
        {
            Task obj = new Task();
            obj.OrderId=sand.OrderId;
            obj.SandBlastingLevel = sand.SandBlastingLevel;
            obj.Notes = sand.Notes;
            obj.Status = "Soldering";
            obj.ImageUrl = sand.ImageUrl;
            obj.CreatedAt = sand.CreatedAt;
            _context.Tasks.Add(obj);
            _context.SaveChanges();
            return true;
        }

        public List<Orders> GetPaint()
        {
            var orders = _context.OrderDetails.Where(o => o.Status == "Painting").ToList();
            return orders;
        }
        public List<Task> GetAllPaint()
        {
            var tas = _context.Tasks.Where(o => o.Status == "Soldering").ToList();
            return tas;
        }

        public List<Orders> GetPack()
        {
            var orders = _context.OrderDetails.Where(o => o.Status == "Packaging").ToList();
            return orders;
        }

        public bool AddPaintOrders(PaintDTO p)
        {

            Task obj = new Task();
            obj.OrderId = p.OrderId;
            obj.Notes = p.Notes;
            obj.Status = "Painting";
            obj.PColor = p.PColor;
            obj.PType = p.PType;
            obj.ImageUrl = p.ImageUrl;
            obj.CreatedAt = p.CreatedAt;
            _context.Tasks.Add(obj);
            _context.SaveChanges();
            return true;
        }
        

        public bool AddPackOrders(PackDTO p)
        {

            Task obj = new Task();
            obj.OrderId = p.OrderId;
            obj.Notes = p.Notes;
            obj.Status ="Packaging";
            obj.IRating = p.IRating;
            obj.ImageUrl = p.ImageUrl;
            obj.CreatedAt = p.CreatedAt;
            _context.Tasks.Add(obj);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateTask(int id, Task t)
        {
            var task = _context.Tasks.Find(id);

            if (task != null)
            {
                task.SandBlastingLevel = t.SandBlastingLevel;
                task.Status = t.Status;
                task.ImageUrl = t.ImageUrl;
                task.Notes = t.Notes;
                task.IRating= t.IRating;
                task.PColor = t.PColor;
                task.PType = t.PType;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
