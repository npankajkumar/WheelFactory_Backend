using Microsoft.AspNetCore.Mvc;
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
        public TaskController(WheelContext wc)
        {
            _task = new TaskService(wc);
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return _task.GetTask();
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Task Get(int id)
        {
            return _task.GetTaskById(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Task value)
        {
            var t = _task.AddTask(value);
            if (value != null)
            {
                return Ok(value); 
            }
            return BadRequest();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Task value)
        {
            var t= _task.UpdateTask(id,value);
            if(value != null)
            {
                    return Ok(t);
            }
            return BadRequest();

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_task.DeleteTask(id))
            {
                return Ok();
            }
            return BadRequest();    

        }
    }
}
