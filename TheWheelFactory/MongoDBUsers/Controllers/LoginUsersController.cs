using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Repositories;
using MongoDBUsers.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDBUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUsersController : ControllerBase
    {
        private readonly LoginUsersContext _context=new LoginUsersContext();
        // GET: api/<LoginUsersController>
        [HttpGet]
        public IEnumerable<LoginUsers> Get()
        {
            return _context.GetUsers();
        }

        // GET api/<LoginUsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginUsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LoginUsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginUsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
