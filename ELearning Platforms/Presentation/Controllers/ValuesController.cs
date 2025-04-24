using ELearning_Platforms.Application.DTOs;
using ELearning_Platforms.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearning_Platforms.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IStudentService _Service;

        public ValuesController(IStudentService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponseDTO>>>GetStudentByIdAsync(string Id)
        {
            return Ok(await _ser)
        }
    }
}
