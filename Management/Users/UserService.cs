using AutoMapper;
using Management.Roles.Model;
using Management.Users.Dto;
using Management.Users.Model;
using Microsoft.EntityFrameworkCore;
using StudentWebApi;

namespace Management.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserDetailDto GetById(int userId)
        {
            var user = _context.Users.Include(u => u.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(u => u.Id == userId && !u.IsDeleted);
            return _mapper.Map<UserDetailDto>(user);
        }

        public List<UserDto> GetAll()
        {
            var users = _context.Users.Include(u => u.UserRoles).ThenInclude(r => r.Role).Where(u => !u.IsDeleted).ToList();
            return _mapper.Map<List<UserDto>>(users);
        }

        public void Create(UserCreateDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            _context.Users.Add(user);
            _context.SaveChanges();

            var userRoles = new List<UserRole>();
            foreach (var roleId in createUserDto.RoleIds)
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
                if (role == null)
                {
                    throw new ArgumentException($"Role with ID {roleId} does not exist.");
                }
                var userRole = new UserRole
                {
                    RoleId = roleId,
                    UserId = user.Id
                };
                userRoles.Add(userRole);
            }

            _context.UserRoles.AddRange(userRoles);
            _context.SaveChanges();
        }

        public void Update(int id, UserUpdateDto updatedUserDto)
        {
            var usersIds = _context.Users.Include(u => u.UserRoles).ThenInclude(r => r.Role).Where(u => u.Id == id).ToList();

            foreach (var userId in usersIds)
            {
                _context.Users.Remove(userId);
            }

            var user = _mapper.Map<User>(updatedUserDto);
            _context.Users.Add(user);
            _context.SaveChanges();

            var userRoles = new List<UserRole>();
            foreach (var roleId in updatedUserDto.RoleIds)
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
                var userRole = new UserRole
                {
                    RoleId = roleId,
                    UserId = user.Id
                };
                userRoles.Add(userRole);
            }

            _context.UserRoles.AddRange(userRoles);
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefault(u => u.Id == userId);
            user.IsDeleted = true;
            if (user.UserRoles != null)
            {
                foreach (var userRole in user.UserRoles)
                {
                    userRole.IsDeleted = true;
                }
            }
            _context.SaveChanges();
        }
    }

}
