using AutoMapper;
using Management.Extentions.TokenHelper;
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
        private readonly TokenHelper _tokenHelper;


        public RoleService(IMapper mapper, AppDbContext context, TokenHelper tokenHelper)
        {
            _mapper = mapper;
            _context = context;
            _tokenHelper = tokenHelper;
        }

        public RoleDetailDto GetById(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId && !r.IsDeleted);
            return _mapper.Map<RoleDetailDto>(role);
        }

        public List<RoleDto> GetAll()
        {
            var roles = _context.Roles.Where(r => !r.IsDeleted).ToList();
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public void Create(RoleCreateDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);
            _context.Roles.Add(role);
            _context.SaveChanges();
            var userRoles = new List <UserRole>();

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

        public void Update(int id, RoleUpdateDto updatedRoleDto)
        {
            var rolesIds = _context.Roles.Where(r => r.Id == id).ToList();

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
                .FirstOrDefault(r => r.Id == roleId);
            if (role == null)
            {
                throw new InvalidOperationException($"Role with ID {roleId} not found.");
            }
            else
            {
                role.DeletedBy = _tokenHelper.GetUserIdFromContext();
                role.DeletedOn=DateTime.UtcNow;
                role.IsDeleted = true;
                _context.SaveChanges();
            }
           
        }
    }

}

