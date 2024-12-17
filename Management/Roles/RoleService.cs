using AutoMapper;
using Management.Extentions.TokenHelper;
using Management.Roles.Dto;
using Management.Roles.Model;
using Management.Users.Model;
using Microsoft.EntityFrameworkCore;
using StudentWebApi;

namespace Management.Roles
{

    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenHelper _tokenHelper;


        public RoleService(IMapper mapper, AppDbContext context, TokenHelper tokenHelper)
        {
            _mapper = mapper;
            _context = context;
            _tokenHelper = tokenHelper;
        }

        public async Task <RoleDetailDto> GetByIdAsync(int roleId)
        {
            var role =await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId && !r.IsDeleted);
            return _mapper.Map<RoleDetailDto>(role);
        }

        public async Task< List<RoleDto>> GetAllAsync()
        {

            var roles = await _context.Roles.Where(r => !r.IsDeleted).ToListAsync();
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task CreateAsync(RoleCreateDto createRoleDto)
        {

            var role = _mapper.Map<Role>(createRoleDto);
            role.CreatedBy= _tokenHelper.GetUserIdFromContext();
            role.CreatedOn = DateTime.UtcNow;
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, RoleUpdateDto updatedRoleDto)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role is null)
            {
                throw new Exception("Role not found!");
            }
            else
            {
                var newRole = _mapper.Map(updatedRoleDto, role);
                role.UpdatedBy = _tokenHelper.GetUserIdFromContext();
                _context.Roles.Update(newRole);
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(int roleId)
        {
            var role = await _context.Roles
                                     .FirstOrDefaultAsync(r => r.Id == roleId);
            if (role == null)
            {
                throw new InvalidOperationException($"Role with ID {roleId} not found.");
            }
            else
            {
                role.DeletedBy = _tokenHelper.GetUserIdFromContext();
                role.DeletedOn = DateTime.UtcNow;
                role.IsDeleted = true;

                await _context.SaveChangesAsync();
            }
        }

    }

}

