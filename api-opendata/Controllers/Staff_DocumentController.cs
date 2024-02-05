using api_opendata.Data;
using api_opendata.Dto;
using api_opendata.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_opendata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class Staff_DocumentController : ControllerBase
    {
        private readonly Staff_DocumentService _service;

        public Staff_DocumentController(Staff_DocumentService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<Staff_Document>> Save(Staff_DocumentDto dto)
        {
            var res = await _service.SaveAsync(dto);
            if (res == true)
            {
                return Ok(new { message = "Staff_Document: Dữ liệu đã được lưu", id = res });
            }
            else
            {
                return BadRequest(new { message = "Staff_Document: Lỗi lưu dữ liệu", error = true });
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<ActionResult<Staff_Document>> Delete(int Id)
        {
            var res = await _service.DeleteAsync(Id);
            if (res == true)
            {
                return Ok(new { message = "Staff_Document: Dữ liệu đã được xóa" });
            }
            else
            {
                return BadRequest(new { message = "Staff_Document: Lỗi xóa dữ liệu", error = true });
            }
        }
    }
}
