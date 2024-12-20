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
            var role = await _context.Roles.Include(u => u.UserRoles)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(r => r.Id == roleId && !r.IsDeleted);
            return _mapper.Map<RoleDetailDto>(role);
        }
        public async Task< List<RoleDto>> GetAllAsync()
        {
            var roles = await _context.Roles.Include(u => u.UserRoles)
                .ThenInclude(u => u.User)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
            return _mapper.Map<List<RoleDto>>(roles);
        }
        public async Task CreateAsync(RoleCreateDto createRoleDto)
        {

            var role = _mapper.Map<Role>(createRoleDto);
            role.CreatedBy= _tokenHelper.GetUserIdFromContext();
            role.CreatedOn = DateTime.UtcNow;
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            var userRoles = new List<UserRole>();

            foreach (var userId in createRoleDto.UserIds)
            {
                var user =await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with ID {userId} does not exist.");
                }
                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = role.Id,
                };
                userRoles.Add(userRole);
            }
            _context.UserRoles.AddRangeAsync(userRoles);
            _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, RoleUpdateDto updatedRoleDto)
        {
            var role = await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                throw new Exception("Role not found!");
            }

            _mapper.Map(updatedRoleDto, role); 
            role.UpdatedBy = _tokenHelper.GetUserIdFromContext();
            role.UpdatedOn = DateTime.UtcNow;

            var newUserRoles = new List<UserRole>();
            foreach (var userId in updatedRoleDto.UserIds)
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with ID {userId} does not exist.");
                }

                var existingUserRole = role.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
                if (existingUserRole == null)
                {
                    var userRole = new UserRole
                    {
                        UserId = userId,
                        RoleId = role.Id
                    };
                    newUserRoles.Add(userRole);
                }
            }

            if (newUserRoles.Any())
            {
                await _context.UserRoles.AddRangeAsync(newUserRoles);
            }

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int roleId)
        {
            var role = await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                throw new InvalidOperationException($"Role with ID {roleId} not found.");
            }

            if (role.UserRoles != null)
            {
                foreach (var userRole in role.UserRoles)
                {
                    userRole.IsDeleted = true;
                }
            }

            role.DeletedBy = _tokenHelper.GetUserIdFromContext(); 
            role.DeletedOn = DateTime.UtcNow; 
            role.IsDeleted = true; 

            await _context.SaveChangesAsync();
        }

    }

}

