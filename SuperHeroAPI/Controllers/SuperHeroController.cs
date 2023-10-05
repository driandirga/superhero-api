using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var heros = new List<SuperHero>
            {
                new SuperHero {
                    Id = 1,
                    Name = "Spederman",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Power = "Spider Webs",
                    IsFly = true,
                }
            };
        }
    }
}
