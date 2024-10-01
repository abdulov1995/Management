using AutoMapper;
using Management.Roles.Dto;
using Management.Roles.Model;
using Microsoft.EntityFrameworkCore;
using StudentWebApi;

namespace Management.Roles
{

    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public RoleDetailDto GetById(int roleId)
        {
            var role = _context.Roles.Include(u => u.UserRoles).ThenInclude(u => u.User).FirstOrDefault(r => r.Id == roleId && !r.IsDeleted);
            return _mapper.Map<RoleDetailDto>(role);
        }

        public List<RoleDto> GetAll()
        {
            var roles = _context.Roles.Include(u => u.UserRoles).ThenInclude(u => u.User).Where(r => !r.IsDeleted).ToList();
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public void Create(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);
            _context.Roles.Add(role);
            _context.SaveChanges();
            var userRoles = new List<UserRole>();

            foreach (var userId in createRoleDto.UserIds)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
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
            _context.UserRoles.AddRange(userRoles);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateRoleDto updatedRoleDto)
        {
            var rolesIds = _context.Roles.Include(u => u.UserRoles).ThenInclude(r => r.User).Where(r => r.Id == id).ToList();

            foreach (var roleId in rolesIds)
            {
                _context.Roles.Remove(roleId);
            }
            var role = _mapper.Map<Role>(updatedRoleDto);
            _context.Roles.Add(role);
            _context.SaveChanges();
            var userRoles = new List<UserRole>();

            foreach (var userId in updatedRoleDto.UserIds)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
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
            _context.UserRoles.AddRange(userRoles);
            _context.SaveChanges();
        }

        public void Delete(int roleId)
        {
            var role = _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefault(r => r.Id == roleId);
            role.IsDeleted = true;

            if (role.UserRoles != null)
            {
                foreach (var userRole in role.UserRoles)
                {
                    userRole.IsDeleted = true;
                }
            }
            _context.SaveChanges();
        }
    }

}

