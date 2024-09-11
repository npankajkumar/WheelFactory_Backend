using Microsoft.EntityFrameworkCore;
using WheelFactory.Models;
using Task = WheelFactory.Models.Task;

namespace WheelFactory.Services
{
    public class TaskService
    {
        private readonly WheelContext _context;
        public TaskService(WheelContext context)
        {
            _context = context;
        }
        public List<Task> GetTask()
        {
            var tasks=(_context.Tasks.ToList());
            return (tasks);
             
        }
        public Task GetTaskById(int id)
        {
            var tasks = _context.Tasks.Find(id);
            return (tasks);

        }
        public bool AddTask(Task task)
        { 
            _context.Tasks.Add(task);
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
