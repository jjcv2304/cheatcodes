using CheatCodes.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheatCodes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController:ControllerBase
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
        }
    }
}