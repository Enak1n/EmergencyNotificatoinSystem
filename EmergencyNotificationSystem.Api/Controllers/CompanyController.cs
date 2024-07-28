using EmergencyNotificationSystem.Domain.Interfaces.Services;
using EmergencyNotificationSystem.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotificationSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAll();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _companyService.GetById(id);

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyDto companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");

            await _companyService.Create(Guid.NewGuid(), DateTime.UtcNow, companyDto.name);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyService.Delete(id);

            return Ok();
        }
    }
}
