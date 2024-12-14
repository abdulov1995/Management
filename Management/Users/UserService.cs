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
using System.Threading.Tasks;

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
        public async Task<UserDetailDto> GetByIdAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
            return _mapper.Map<UserDetailDto>(user);
        }
        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.Include(u => u.Role)
                                            .Where(u => !u.IsDeleted)
                                            .ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<User> CreateAsync(UserCreateDto createUserDto)
        {
            var existingUser = await _context.Users
                                 .FirstOrDefaultAsync(u => u.Email == createUserDto.Email || u.UserName == createUserDto.UserName);
            if (existingUser == null)
            {
                throw new Exception("User with the same email or username already exists.");
            }
            else
            {
                var user = _mapper.Map<User>(createUserDto);
                var userId = _tokenHelper.GetUserIdFromContext();

                user.CreatedBy = userId;
                user.Password = PasswordHelper.CreateMd5(user.Password);
                user.CreatedOn = DateTime.UtcNow;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
        }
        public async Task UpdateAsync(int id, UserUpdateDto updatedUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                throw new Exception("User not found!");
            }
            else
            {
                var newUser = _mapper.Map(updatedUserDto, user);
                var userId = _tokenHelper.GetUserIdFromContext();
                newUser.UpdatedBy = userId;
                newUser.Password = PasswordHelper.CreateMd5(newUser.Password);

                _context.Users.Update(newUser);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                throw new Exception("User not found!");
            }
            else
            {
                user.IsDeleted = true;
                user.DeletedBy = _tokenHelper.GetUserIdFromContext();
                user.DeletedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
    }
}
