using api_opendata.Data;
using api_opendata.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_opendata.Service
{
    public class DocumentService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AspNetUsers> _userManager;

        public DocumentService(DatabaseContext context, IMapper mapper, IHttpContextAccessor httpContext, UserManager<AspNetUsers> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public async Task<List<DocumentDto>> GetAllAsync()
        {
            var item = await _context.Document!.Where(x => x.IsDeleted == false).ToListAsync();

            var departmentDtos = _mapper.Map<List<DocumentDto>>(item);

            return departmentDtos;
        }

        public async Task<int> SaveAsync(DocumentDto dto)
        {
            int id = 0;
            var currentUser = await _userManager.GetUserAsync(_httpContext.HttpContext!.User);
            Document item = null; // Declare item variable

            // Retrieve an existing item based on Id or if dto.Id is 0
            var existingItem = await _context.Document!.FirstOrDefaultAsync(d => d.Id == dto.Id && d.IsDeleted == false);

            if (existingItem == null || dto.Id == null)
            {
                // If the item doesn't exist or dto.Id is 0, create a new item
                item = _mapper.Map<Document>(dto);
                item.IsDeleted = false;
                item.CreatedTime = DateTime.Now;
                item.CreatedUser = currentUser != null ? currentUser.UserName : null;
                _context.Document!.Add(item);
            }
            else
            {
                // If the item exists, update it with values from the dto
                item = existingItem;
                _mapper.Map(dto, item);
                item.IsDeleted = false;
                item.UpdatedTime = DateTime.Now;
                item.UpdatedUser = currentUser != null ? currentUser.UserName : null;
                _context.Document!.Update(item);
            }

            // Save changes to the database
            var res = await _context.SaveChangesAsync();

            // Simplified assignment of id based on the result of SaveChanges
            id = (int)(res > 0 ? item.Id : 0);

            // Return the id
            return id;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            // Retrieve an existing item based on Id
            var existingItem = await _context.Document!.FirstOrDefaultAsync(d => d.Id == Id && d.IsDeleted == false);
            var currentUser = await _userManager.GetUserAsync(_httpContext.HttpContext!.User);

            if (existingItem == null) { return false; } // If the item doesn't exist, return false

            existingItem!.IsDeleted = true; // Mark the item as deleted
            existingItem.UpdatedTime = DateTime.Now;
            existingItem.UpdatedUser = currentUser != null ? currentUser.UserName : null;
            _context.Document!.Update(existingItem);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return true to indicate successful deletion
            return true;
        }
    }
}
