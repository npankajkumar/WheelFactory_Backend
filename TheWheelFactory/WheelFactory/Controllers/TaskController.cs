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
        public IActionResult PostOrdersSoldering([FromBody] SandDTO value)
        {
            if (_task.AddSandOrders(value))
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
        public IActionResult PostOrdersPainting([FromBody] PaintDTO value)
        {
            if (_task.AddPaintOrders(value))
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
        public IActionResult PostOrdersPackaging([FromBody] PackDTO value)
        {
             
            if (_task.AddPackOrders(value))
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


