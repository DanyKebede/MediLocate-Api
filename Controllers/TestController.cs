using mediAPI.Data;
using mediAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mediAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        public MediDbContext _dbContext { get; set; }

        public TestController(MediDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult CheckTest()
        {
            throw new Exception("Error goes here");
        }
        [HttpGet("{id}")]
        public IActionResult CheckTest2()
        {
            var response = new ApiError(400, "Medicine Not Found");
            return new JsonResult(response) { StatusCode = response.StatusCode };
        }
        [HttpGet("get-medicines")]
        public async Task<IActionResult> GetMedicines()
        {
            var medicines = await _dbContext.Medicines.ToListAsync();
            return Ok(medicines);
        }

    }
}
