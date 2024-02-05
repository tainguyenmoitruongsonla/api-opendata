using api_opendata.Data;
using api_opendata.Dto;
using api_opendata.Service;
using Microsoft.AspNetCore.Mvc;

namespace api_opendata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _service;

        public DepartmentController(DepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<DepartmentDto>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<Department>> Save(DepartmentDto dto)
        {
            var res = await _service.SaveAsync(dto);
            if (res > 0)
            {
                return Ok(new { message = "Department: Dữ liệu đã được lưu", id = res });
            }
            else
            {
                return BadRequest(new { message = "Department: Lỗi lưu dữ liệu", error = true });
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<ActionResult<Department>> Delete(int Id)
        {
            var res = await _service.DeleteAsync(Id);
            if (res == true)
            {
                return Ok(new { message = "Department: Dữ liệu đã được xóa" });
            }
            else
            {
                return BadRequest(new { message = "Department: Lỗi xóa dữ liệu", error = true });
            }
        }
    }
}
