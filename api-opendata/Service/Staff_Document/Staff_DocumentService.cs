using api_opendata.Data;
using api_opendata.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_opendata.Service
{
    public class Staff_DocumentService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AspNetUsers> _userManager;

        public Staff_DocumentService(DatabaseContext context, IMapper mapper, IHttpContextAccessor httpContext, UserManager<AspNetUsers> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public async Task<bool> SaveAsync(Staff_DocumentDto dto)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContext.HttpContext!.User);
            Staff_Document item = null; // Declare item variable

            // Retrieve an existing item based on Id or if dto.Id is 0
            var existingItem = await _context.Staff_Document!.FirstOrDefaultAsync(d => d.Id == dto.Id);

            if (existingItem == null || dto.Id == 0)
            {
                // If the item doesn't exist or dto.Staff_DocumentId is 0, create a new item
                item = _mapper.Map<Staff_Document>(dto);
                _context.Staff_Document!.Add(item);
            }
            else
            {
                // If the item exists, update it with values from the dto
                item = existingItem;
                _mapper.Map(dto, item);
                _context.Staff_Document!.Update(item);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the id
            return true;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var item = await _context.Staff_Document!.FirstOrDefaultAsync(d => d.Id == Id);

            if (item == null) { return false; }
            _context.Staff_Document.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
