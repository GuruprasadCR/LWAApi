using LWAApi.Models;
using LWAApi.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LWAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEnrollController : ControllerBase

    {
        
        readonly DatabaseContext _dbContext = new();

        public StudentEnrollController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }




        // Select
        [HttpGet]
        [Route("Select")]
        public IQueryable Get1()
        {

            var data1 = from k in _dbContext.Student
                        select new
                        {
                            StudentName = k.Studentname,
                            Studentmarks = k.Studentmarks
                        };

            return data1;
        }


        // Where
        [HttpGet]
        [Route("where")]
        public IQueryable Get2()
        {

            var data2 = from k in _dbContext.Student
                        where k.Studentmarks < 93
                        select new
                        {
                            StudentName = k.Studentname,
                            Studentmarks = k.Studentmarks
                        };

            return data2;
        }

    

        //orderby query based
        [HttpGet]
        [Route("orderby")]
        public IQueryable Get3()
        {

            var data3 = from c in _dbContext.Student
                        orderby c.Studentmarks
                        select new
                        { 
                            Name=c.Studentname,
                            Marks=c.Studentmarks };

            return data3;

        }


        
        [HttpGet]
        [Route("Multiorder")]
        public IQueryable Get6()
        {
           

            var data6 = from c in _dbContext.Student
                        orderby c.Studentmarks, c.Studentname descending 
                        select new
                        {
                            Marks = c.Studentmarks,
                            Name = c.Studentname
                            
                        };

            return data6;

        }

        //innerjoin
        [HttpGet("Inner")]
        public IQueryable Get4()
        {

            var data4 = from s in _dbContext.Student
                       join d in _dbContext.Department
                       on s.Studentdepartment equals d.DepID
                       select new
                       {
                           StudentName = s.Studentname,
                           StudentID = s.StudentID,
                           Studentadress = s.Studentaddress,
                           DepartmentName = d.Depname
                       };
            return data4;
        }

        //outer join 
        [HttpGet("Outer")]
        public IQueryable Get5()
        {

            var data5 = from s in _dbContext.Student
                       join d in _dbContext.Department
                       on s.Studentdepartment equals d.DepID into groupedstudent
                       from gc in groupedstudent.DefaultIfEmpty()
                       select new
                       {
                           StudentName = s.Studentname,
                           StudentID = s.StudentID,
                           Studentadress = s.Studentaddress,
                           Department = gc == null ? "Nodepartment" : gc.Depname
                       };
            return data5;
        }





    }
}
































