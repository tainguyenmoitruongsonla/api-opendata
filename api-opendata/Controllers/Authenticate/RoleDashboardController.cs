﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api_opendata.Data;
using api_opendata.Dto;
using api_opendata.Service;

namespace api_opendata.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class RoleDashboardController : ControllerBase
    {
        private readonly RoleDashboardService _service;

        public RoleDashboardController(RoleDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<RoleDashboardDto>> GetAllRoleDashboard()
        {
            return (await _service.GetAllRoleDashboardAsync());
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<RoleDashboardDto> GetRoleDashboardById(int Id)
        {
            return await _service.GetRoleDashboardByIdAsync(Id);
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<RoleDashboards>> SaveRoleDashboard(RoleDashboardDto moddel)
        {
            var res = await _service.SaveRoleDashboardAsync(moddel);
            if (res == true)
            {
                return Ok(new { message = "Saved role-dashboard successfully" });
            }
            else
            {
                return BadRequest(new { message = "Save role-dashboard failed", });
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult<RoleDashboards>> DeleteRoleDashboard(RoleDashboardDto moddel)
        {
            var res = await _service.DeleteRoleDashboardAsync(moddel);
            if (res == true)
            {
                return Ok(new { message = "Role-dashboard successfully deleted" });
            }
            else
            {
                return BadRequest(new { message = "Removing role-dashboards failed", error = true });
            }
        }
    }
}
