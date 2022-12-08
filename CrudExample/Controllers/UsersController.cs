using CrudExample.Models;
using CrudExample.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<UserModel>> Get()
        {
            await Task.Delay(4000);
            return _userService.Get();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(string id)
        {
            await Task.Delay(4000);
            var model = _userService.Get(id);
            if (model == null) return NotFound();

            return Ok(model);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel input)
        {
            await Task.Delay(4000);
            var output = _userService.Add(input);
            return Ok(output);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Put(string id, [FromBody] UserModel input)
        {
            await Task.Delay(4000);
            var output = _userService.Update(input);
            if (output == null) return NotFound();

            return Ok(output);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            await Task.Delay(4000);
            var model = _userService.Get(id);
            if(model == null) return NotFound();

            _userService.Delete(id);
            return Ok();
        }
    }
}
