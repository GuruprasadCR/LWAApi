using LWAApi.Models;
using LWAApi.Repositories;
using LWAApi.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LWAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase

    {

        private readonly IUserRepository _IUserRepository;
       readonly Logger loggerx = LogManager.GetCurrentClassLogger();


        public UsersController(IUserRepository IUserRepository)
        {
            _IUserRepository = IUserRepository;
            
        }

        


        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            loggerx.Info("User get accessed");

            return await Task.FromResult(_IUserRepository.GetUsers());

            

        }

        // GET api/<UsersController>/5
        [HttpGet("{UserId}")]
        public async Task<ActionResult<User>> Get(int UserId)
        {
            loggerx.Info("User get by UserId accessed");
            var user = await Task.FromResult(_IUserRepository.GetUser(UserId));

           

            if (user == null)
            {
               
                return NotFound();
            }
           
            
            return user;
        }
        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post(User User)
        {
            _IUserRepository.CreateUser(User);
            return await Task.FromResult(User);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{UserId}")]
        public async Task<ActionResult<User>> Put(int UserId, User User)
        {
            if (UserId != User.UserId)
            {
                return BadRequest();
            }
            try
            {
                _IUserRepository.UpdateUser(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(User);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{UserId}")]
        public async Task<ActionResult<User>> Delete(int UserId)
        {
            var user = _IUserRepository.DeleteUser(UserId);
            return await Task.FromResult(user);
        }


        private bool UserExists(int UserId)
        {
            return _IUserRepository.DoesUserEixsts(UserId);
        }
    }
}
