using AutoMapper;
using Management.Auth.Dto;
using Management.Roles.Model;
using Management.Users.Dto;
using Management.Users.Model;
using Microsoft.EntityFrameworkCore;
using StudentWebApi;
using System.Text.RegularExpressions;

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
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == userId && !u.IsDeleted);
            return _mapper.Map<UserDetailDto>(user);
        }

        public List<UserDto> GetAll()
        {
            var users = _context.Users.Include(u => u.Role).Where(u => !u.IsDeleted).ToList();
            return _mapper.Map<List<UserDto>>(users);
        }

        public User Create(UserCreateDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(int id, UserUpdateDto updatedUserDto)
        {
            var usersIds = _context.Users.Include(u => u.Role).Where(u => u.Id == id).ToList();

            foreach (var userId in usersIds)
            {
                _context.Users.Remove(userId);
            }

            var user = _mapper.Map<User>(updatedUserDto);
            _context.Users.Add(user);
            _context.SaveChanges();

        }

        public void Delete(int userId)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == userId);
            user.IsDeleted = true;
            _context.SaveChanges();
        }
    }

}
