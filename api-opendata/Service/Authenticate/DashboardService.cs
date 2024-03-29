﻿using api_opendata.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using api_opendata.Dto;

namespace api_opendata.Service
{
    public class DashboardService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public DashboardService(DatabaseContext context, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<List<DashboardDto>> GetAllDashboardAsync()
        {
            var dashboards = await _context!.Dashboards!.OrderBy(x => x.Name).ToListAsync();
            var dashboardModels = _mapper.Map<List<DashboardDto>>(dashboards);

            var allFunctions = await _context!.Functions!.ToListAsync();
            foreach (var dashboardModel in dashboardModels)
            {
                dashboardModel.Functions = _mapper.Map<List<FunctionDto>>(allFunctions);
            }

            return dashboardModels;
        }

        public async Task<List<RoleDashboardDto>> GetDashboardByRoleAsync(string roleName)
        {
            var role = await _context!.Roles!.FirstOrDefaultAsync(x => x!.Name!.ToLower() == roleName.ToLower());
            var dashboards = await _context!.Dashboards!.Where(x => x.IsDeleted == false).ToListAsync();
            var roleDashboards = new List<RoleDashboardDto>();

            foreach (var dashboard in dashboards)
            {
                var rdash = await _context!.RoleDashboards!
                    .FirstOrDefaultAsync(x => x.RoleName == roleName && x.DashboardId == dashboard.Id);

                var model = new RoleDashboardDto
                {
                    DashboardId = dashboard.Id,
                    DashboardName = dashboard.Name,
                    FileControl = dashboard.Path,
                    RoleId = role?.Id
                };

                if (rdash != null)
                {
                    model.Id = rdash.Id;
                    model.RoleId = rdash.RoleId;
                    model.RoleName = rdash.RoleName;
                    model.PermitAccess = (bool)rdash.PermitAccess!;
                }
                else
                {
                    model.RoleId = role?.Id;
                    model.RoleName = role?.Name;
                    model.PermitAccess = false;
                }

                roleDashboards.Add(model);
            }

            return _mapper.Map<List<RoleDashboardDto>>(roleDashboards);
        }


        public async Task<List<UserDashboardDto>> GetDashboardByUserAsync(string userName)
        {
            var user = await _context!.Users!.FirstOrDefaultAsync(x => x!.UserName!.ToLower() == userName.ToLower());
            var dashboards = await _context!.Dashboards!.Where(x => x.IsDeleted == false).ToListAsync();
            var userDashboards = new List<UserDashboardDto>();

            foreach (var dashboard in dashboards)
            {
                var udash = await _context!.UserDashboards!
                    .FirstOrDefaultAsync(x => x.UserName == userName && x.DashboardId == dashboard.Id);

                var model = new UserDashboardDto
                {
                    DashboardId = dashboard.Id,
                    DashboardName = dashboard.Name,
                    FileControl = dashboard.Path,
                    UserId = user?.Id
                };

                if (udash != null)
                {
                    model.Id = udash.Id;
                    model.UserId = udash.UserId;
                    model.UserName = udash.UserName;
                    model.PermitAccess = (bool)udash.PermitAccess!;
                }
                else
                {
                    model.UserId = user?.Id;
                    model.UserName = user?.UserName;
                    model.PermitAccess = false;
                }

                userDashboards.Add(model);
            }

            return _mapper.Map<List<UserDashboardDto>>(userDashboards);
        }

        public async Task<DashboardDto?> GetDashboardByIdAsync(int Id)
        {
            var item = await _context!.Dashboards!.FindAsync(Id);
            var dash = _mapper.Map<DashboardDto>(item);
            var functions = await _context!.Functions!.Where(x => x.Id > 0).ToListAsync();
            dash.Functions = _mapper.Map<List<FunctionDto>>(functions);

            return dash;
        }


        public async Task<bool> SaveDashboardAsync(DashboardDto model)
        {
            var existingItem = await _context.Dashboards!.FirstOrDefaultAsync(d => d.Id == model.Id);

            if (existingItem == null || model.Id == 0)
            {
                var newItem = _mapper.Map<Dashboards>(model);
                newItem.IsDeleted = false;
                newItem.CreatedTime = DateTime.Now;
                newItem.CreatedUser = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? null;
                _context.Dashboards!.Add(newItem);
            }
            else
            {
                var updateItem = await _context.Dashboards!.FirstOrDefaultAsync(d => d.Id == model.Id);

                updateItem = _mapper.Map(model, updateItem);

                updateItem!.ModifiedTime = DateTime.Now;
                updateItem.ModifiedUser = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? null;
                _context.Dashboards!.Update(updateItem);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDashboardAsync(int Id)
        {
            var existingItem = await _context.Dashboards!.FirstOrDefaultAsync(d => d.Id == Id);

            if (existingItem == null) { return false; }

            existingItem!.IsDeleted = true;
            _context.Dashboards!.Update(existingItem);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
