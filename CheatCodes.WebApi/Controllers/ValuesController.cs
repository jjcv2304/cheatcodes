using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheatCodes.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheatCodes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public Category Get()
        {
            var category = new Category()
            {
                Id = 0,
                Name = "C#",
                Description = "Programing language"
            };
            return category;
            // return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}