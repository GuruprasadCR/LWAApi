using LWAApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LWAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Getplayersdetails : ControllerBase
    {
        readonly DatabaseContextb DBC = new();

        public Getplayersdetails(DatabaseContextb dbContext)
        {
            DBC = dbContext;
        }

  
        [HttpGet]
        public IQueryable Get()

        {
            return DBC.Playerstbl.FromSqlRaw<Players>("getplyersdetails");
        }

    }



}
