using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelFactory.Models;
using WheelFactory.Services;
using Task = WheelFactory.Models.Task;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WheelFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly string _basePath = @"C:\Users\pulkit\Desktop\cc\backend\TheWheelFactory\WheelFactory\wwwroot\images\";

        private readonly TaskService _task;
        private readonly OrdersService _orders;
        private readonly WheelContext _wc;
        public TaskController(WheelContext wc)
        {
            _wc = wc;
            _task = new TaskService(_wc);
            _orders = new OrdersService(wc);
        }

        //GET: api/<ValuesController>
        [HttpGet("packaging/{id}")]
        public List<Task> GetPackagingById(int id)
        {
            return _task.GetPackId(id);

        }
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return _task.GetTask();
        }
        [HttpGet("{id}")]
        public List<Task> Get(int id)
        {
            return _task.GetTaskById(id);
        }


        [HttpGet("soldering")]
        public IActionResult GetOrdersSoldering(int id)
        {
            var sold = _task.GetSold();

            if (sold != null)
            {
                return Ok(sold);
            }
            return BadRequest();

        }


        [HttpGet("soldering/{id}")]
        public List<Task> GetSolderingById(int id)
        {
            return _task.GetSoldId(id);

        }
        [HttpGet("painting/{id}")]
        public List<Task> GetPaintingById(int id)
        {
            return _task.GetPaintId(id);

        }


        




        [HttpPost("soldering")]
        public IActionResult PostOrdersSoldering([FromForm] SandDTO value)
        { 
            try
            {
                if (value.ImageUrl == null || value.ImageUrl.Length == 0)
                    return BadRequest("No file uploaded.");

                var originalFileName =  Path.GetFileName(value.ImageUrl.FileName);
                var filePath = Path.Combine(_basePath, originalFileName);

                if (!Directory.Exists(_basePath))
                {
                    Directory.CreateDirectory(_basePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    value.ImageUrl.CopyToAsync(stream);
                }


            }
            catch { }
            Task t = new Task();
            t.OrderId = value.OrderId;
            t.SandBlastingLevel = value.SandBlastingLevel;
            t.Notes = value.Notes;
            t.Status ="Soldering";
            t.ImageUrl = "http://localhost:5041/images/" + value.ImageUrl.FileName;

            if (_task.AddSandOrders(t))
            {
                var obj = _wc.OrderDetails.Find(value.OrderId);
                obj.Status = "Painting";
                _wc.OrderDetails.Update(obj);
                _wc.SaveChanges();

                return Ok(value);
            }
            return BadRequest();

        }

        [HttpGet("painting")]

        public IActionResult GetOrdersPainting()
        {
            var sold = _task.GetPaint();

            if (sold != null)
            {
                return Ok(sold);
            }
            return BadRequest();

        }
        [HttpPost("painting")]
        public IActionResult PostOrdersPainting([FromForm] PaintDTO value)
        {
            try
            {
                if (value.ImageUrl == null || value.ImageUrl.Length == 0)
                    return BadRequest("No file uploaded.");

                var originalFileName =  Path.GetFileName(value.ImageUrl.FileName);
                var filePath = Path.Combine(_basePath, originalFileName);

                if (!Directory.Exists(_basePath))
                {
                    Directory.CreateDirectory(_basePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    value.ImageUrl.CopyToAsync(stream);
                }


            }
            catch { }
            Task t = new Task();
            t.OrderId = value.OrderId;
            t.PColor = value.PColor;
            t.PType=value.PType;
            t.Notes = value.Notes;
            t.Status = "Painting";
            t.ImageUrl = "http://localhost:5041/images/" + value.ImageUrl.FileName;


            if (_task.AddPaintOrders(t))
            {
                var obj = _wc.OrderDetails.Find(value.OrderId);
                obj.Status = "Packaging";
                _wc.OrderDetails.Update(obj);
                _wc.SaveChanges();
                return Ok(value);
            }
            return BadRequest();
        }


        [HttpGet("packaging")]

        public IActionResult GetOrdersPackaging()
        {
            var sold = _task.GetPack();

            if (sold != null)
            {
                return Ok(sold);
            }
            return BadRequest();

        }
        [HttpGet("allpainting")]
        public IActionResult GetAllPainting()
        {
            var sold = _task.GetAllPaint();

            if (sold != null)
            {
                return Ok(sold);
            }
            return BadRequest();

        }

        [HttpPost("packaging")]
        public IActionResult PostOrdersPackaging([FromForm] PackDTO value)
        {
            try
            {
                if (value.ImageUrl == null || value.ImageUrl.Length == 0)
                    return BadRequest("No file uploaded.");

                var originalFileName =Path.GetFileName(value.ImageUrl.FileName);
                var filePath = Path.Combine(_basePath, originalFileName);

                if (!Directory.Exists(_basePath))
                {
                    Directory.CreateDirectory(_basePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    value.ImageUrl.CopyToAsync(stream);
                }


            }
            catch { }

            Task t = new Task();
            t.OrderId = value.OrderId;
            t.IRating = value.IRating;
            t.Notes = value.Notes;
            t.Status = "Packaging";
            t.ImageUrl = "http://localhost:5041/images/" + value.ImageUrl.FileName;


            if (_task.AddPackOrders(t))
            {
                var obj = _wc.OrderDetails.Find(value.OrderId);
                obj.Status = "completed";
                _wc.OrderDetails.Update(obj);
                _wc.SaveChanges();
                return Ok(value);
            }
            return BadRequest();
        }


    }
}


