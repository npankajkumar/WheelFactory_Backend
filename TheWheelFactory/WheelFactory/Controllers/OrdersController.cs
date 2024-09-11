using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WheelFactory.Models;
using WheelFactory.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WheelFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService _order;
        private readonly 
        WheelContext _wheelContext;
        public OrdersController(WheelContext wc)
        {
            _order = new OrdersService(wc);
            _wheelContext = wc;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Orders> Get()
        {
            return  _order.GetOrders();
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public Orders Get(int id)
        {
            return _order.GetById(id);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO value)
        {

            if (_order.AddOrders(value))
            {
                
                return Ok(value);
                //return CreatedAtAction(nameof(Get), new { id = value.OrderId }, value);
            }
            return BadRequest();

        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderDTO value)
        {
            if (_order.UpdateOrder(id, value))
            {

               
                    Transaction ob = new Transaction();
                    ob.OrderId = id;
                    ob.Status = value.Status;
                    _wheelContext.Transactions.Add(ob);
                    _wheelContext.SaveChanges();
                    

    

                return Ok(value);
            }
            return BadRequest();

        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_order.DeleteOrder(id))
                return Ok();
            return BadRequest();

        }
    }
}
