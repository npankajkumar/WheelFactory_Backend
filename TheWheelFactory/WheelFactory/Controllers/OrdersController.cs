using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelFactory.Models;
using WheelFactory.Services;
using static NuGet.Packaging.PackagingConstants;
using Task = WheelFactory.Models.Task;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WheelFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly string _basePath = @"C:\Users\pulkit\Desktop\WheelFactory\Backend\backend\TheWheelFactory\WheelFactory\wwwroot\images\";

        private readonly OrdersService _order;
        private readonly WheelContext _wheelContext;
        public OrdersController(WheelContext wc)
        {
            _order = new OrdersService(wc);
            _wheelContext = wc;
        }

        //GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public Orders Get(int id)
        {
            return _order.GetById(id);
        }

        [HttpGet()]
        public List<Orders> GetAllOrders()
        {

            return _order.GetOrders();

        }

        [HttpGet("current")]
        public List<Orders> GetCurrentOrders()
        {
            return _order.GetCurrent();
        }


        [HttpGet("completed")]
        public List<Orders> GetCompletedOrders()
        {
            return _order.GetComplete();
        }

        [HttpGet("scraped")]
        public List<Orders> GetScrapedOrders()
        {
            return _order.GetScraped();
        }




        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromForm] OrderDTO value,int id)
        {

            try
            {
                if (value.ImageUrl == null || value.ImageUrl.Length == 0)
                    return BadRequest("No file uploaded.");

                var originalFileName = Path.GetFileName(value.ImageUrl.FileName);
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
            Orders o = new Orders();
            o.ClientName = value.ClientName;
            o.Year= value.Year;
            o.Make = value.Make;
            o.Model = value.Model;
            o.Notes = value.Notes;
            o.Status = "neworder";
            o.DamageType = value.DamageType;
            o.ImageUrl = "http://localhost:5041/images/" + value.ImageUrl.FileName;

            if (_order.AddOrders(o))
            {
                return Ok(value);
            }
            return BadRequest();

        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] OrderDTO value)
        {
            if (_order.UpdateOrder(id, value.Status))
            {
                return Ok(value);
            }
            return BadRequest();

        }

        [HttpPut("scrap/{id}")]
        public IActionResult PutScrapOrder(int id, [FromForm] OrderDTO value)
        {
            if (_order.ScrapOrder(id, value))
            {
                return Ok(value);
            }
            return BadRequest();

        }


        [HttpGet("Inventory")]
        public IActionResult GetOrdersInventory()
        {
            var orders = _order.GetInventOrders();
            if (orders != null)
            {
                return Ok(orders);
            }
            return BadRequest();
        }
        [HttpPut("Inventory/{id}")]
        public IActionResult PutOrdersInventory([FromForm]OrderDTO value,int id)
        {
            if(_order.UpdateInventOrder(id,value))
            {
                return Ok(value);
            }
            return BadRequest();
        }
    }
}
