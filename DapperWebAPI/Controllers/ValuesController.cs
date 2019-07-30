using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DapperWebAPI.Entities;
using DapperWebAPI.Services;

using DataAccessLayer.EfStructures.Entities;
using  Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DapperWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly DatabaseHelper db;

        public ValuesController (IConfiguration cf, IDbConnection dbConnection)
        {
            configuration = cf;
            db = new DatabaseHelper(cf);
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var parameters = new DynamicParameters();
                parameters.AddDynamicParams(new {Val = 2});
              
            //parameters.Add("@val", 2);
                parameters.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.Output);
          /// parameters.o
           var result = db.ExecuteOutput("[dbo].[sp_Output]", parameters, "result");


            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //var constring = configuration.GetConnectionString("DefaultConnection");
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
