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
using Management.Extentions.Helpers;

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
           
            var user = _mapper.Map<User>(createUserDto);
            var userId = _tokenHelper.GetUserIdFromToken();

            user.CreatedBy = userId;
            user.Password= PasswordHelper.CreateMd5(user.Password);
            user.CreatedOn=DateTime.UtcNow;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Update(int id, UserUpdateDto updatedUserDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user is null)
            {

            }

            var newUser = _mapper.Map(updatedUserDto,user);
            var userId = _tokenHelper.GetUserIdFromToken();
            newUser.UpdatedBy = userId;
            newUser.Password = PasswordHelper.CreateMd5(newUser.Password);

            _context.Users.Update(newUser);
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
