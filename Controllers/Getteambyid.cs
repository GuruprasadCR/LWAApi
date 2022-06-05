using LWAApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LWAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetteamibyidController : ControllerBase

    {

        readonly DatabaseContextb DBContext = new();

        public GetteamibyidController(DatabaseContextb dbContext)
        {
            DBContext = dbContext;
        }

        // GET: api/<ValuesController>
        //[HttpGet]
        //public IEnumerable<Players> Get()
        //{
        //    return Players;   
        //}


        [HttpGet("{ID}")]
        public object Getbyid(int ID)
        {
            var result = DBContext.Playerstbl.FromSqlRaw("getteambyid {0}", ID).ToList().FirstOrDefault();
            return result;

            //var result = DBContext.getteambyid(ID);
            //return result;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        private bool UserExists(int Teamid)

        {
             bool DoesUserEixsts(int Teamid)
            {
                return DBContext.Teamstbl.Any(e => e.Teamid == Teamid);
            }
            UserExists(Teamid);
            return DoesUserEixsts(Teamid);
        }
    }
}
