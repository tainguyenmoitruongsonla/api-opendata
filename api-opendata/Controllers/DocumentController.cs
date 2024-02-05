using api_opendata.Data;
using api_opendata.Dto;
using api_opendata.Service;
using Microsoft.AspNetCore.Mvc;

namespace api_opendata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _service;

        public DocumentController(DocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<DocumentDto>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<Document>> Save(DocumentDto dto)
        {
            var res = await _service.SaveAsync(dto);
            if (res > 0)
            {
                return Ok(new { message = "Document: Dữ liệu đã được lưu", id = res });
            }
            else
            {
                return BadRequest(new { message = "Document: Lỗi lưu dữ liệu", error = true });
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<ActionResult<Document>> Delete(int Id)
        {
            var res = await _service.DeleteAsync(Id);
            if (res == true)
            {
                return Ok(new { message = "Document: Dữ liệu đã được xóa" });
            }
            else
            {
                return BadRequest(new { message = "Document: Lỗi xóa dữ liệu", error = true });
            }
        }
    }
}
