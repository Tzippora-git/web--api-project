using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.BLL;
using WebApplication2.Models.DTO;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorBLL _donorBll;

        public DonorController(IDonorBLL donorBll) => _donorBll = donorBll;

        [HttpGet] // פתוח לכולם
        public IActionResult GetAll() => Ok(_donorBll.GetAllDonors());

        // WebApplication2/Controllers/DonorController.cs
        [HttpGet("search")] // פתוח לכולם
        public IActionResult Search([FromQuery] string? name, [FromQuery] string? email, [FromQuery] string? giftName)
        {
            var results = _donorBll.GetDonorsByFilter(name, email, giftName);
            return Ok(results);
        }

        [Authorize(Roles = "Manager")] // רק למנהל
        [HttpPost]
        public IActionResult Add([FromBody] donorDTO donor)
        {
            _donorBll.AddDonor(donor);
            return Ok(new { message = "התורם נוסף בהצלחה!" });
        }

        [Authorize(Roles = "Manager")] // רק למנהל
        [HttpPut]
        public IActionResult Update([FromBody] donorDTO donor)
        {
            _donorBll.UpdateDonor(donor);
            return Ok(new { message = "התורם עודכן בהצלחה!" });
        }

        [Authorize(Roles = "Manager")] // רק למנהל
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _donorBll.DeleteDonor(id);
            return Ok(new { message = "התורם נמחק בהצלחה!" });
        }
    }
}