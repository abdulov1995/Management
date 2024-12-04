using AutoMapper;
using Management.Auth.Dto;
using Management.Roles.Model;
using Management.Users.Dto;
using Management.Users.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using StudentWebApi;
using System.Text.RegularExpressions;
using System.Security.Claims;
using Management.Extentions.TokenHelper;
using Microsoft.IdentityModel.Tokens;

namespace Management.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly TokenHelper _tokenHelper;


        public UserService(AppDbContext context, IMapper mapper, TokenHelper tokenHelper)
        {
            _context = context;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
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
            var userId = _tokenHelper.GetUserIdFromContext();
            var user = _mapper.Map<User>(createUserDto);
            user.CreatedBy = userId.ToString();
            user.CreatedOn = DateTime.UtcNow;

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(int id, UserUpdateDto updatedUserDto)
        {
            var usersId = _context.Users.Where(u => u.Id == id).ToList();
            //foreach (var userId in usersIds)
            //{
            //    _context.Users.Remove(userId);
            //}

            var user = _mapper.Map<User>(updatedUserDto);
            //user.UpdatedBy=
            user.UpdatedOn = DateTime.UtcNow;
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
