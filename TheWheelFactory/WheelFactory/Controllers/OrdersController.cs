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


        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO value,int id)
        {

            if (_order.AddOrders(value))
            {
                return Ok(value);
            }
            return BadRequest();

        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderDTO value)
        {
            if (_order.UpdateOrder(id, value))
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
        public IActionResult PutOrdersInventory([FromBody]OrderDTO value,int id)
        {
            if(_order.UpdateInventOrder(id,value))
            {
                return Ok(value);
            }
            return BadRequest();
        }
    }
}
