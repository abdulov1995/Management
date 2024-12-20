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
            var user = await _context.Users.Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
            return _mapper.Map<UserDetailDto>(user);
        }
        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
                .Where(u => !u.IsDeleted)
                .ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
        public async Task<User> CreateAsync(UserCreateDto createUserDto)
        {
            var existingUser = await _context.Users
                                 .FirstOrDefaultAsync(u => u.Email == createUserDto.Email || u.UserName == createUserDto.UserName);
            if (existingUser != null)
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

                var userRoles = new List<UserRole>();
                foreach (var roleId in createUserDto.RoleIds)
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
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
                return user;
            }

        }
        public async Task UpdateAsync(int id, UserUpdateDto updatedUserDto)
        {
            var user = await _context.Users
                      .Include(u => u.UserRoles)
                      .ThenInclude(r => r.Role)
                      .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("User not found!");
            }

            var userId = _tokenHelper.GetUserIdFromContext();
            _mapper.Map(updatedUserDto, user);

            user.UpdatedBy = userId;
            user.Password = PasswordHelper.CreateMd5(updatedUserDto.Password);
            user.UpdatedOn = DateTime.UtcNow;

            var existingUserRoles = _context.UserRoles.Where(ur => ur.UserId == user.Id);
            _context.UserRoles.RemoveRange(existingUserRoles);

            var userRoles = updatedUserDto.RoleIds.Select(roleId => new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            }).ToList();

            await _context.UserRoles.AddRangeAsync(userRoles);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                throw new Exception("User not found!");
            }
            else
            {
                foreach (var userRole in user.UserRoles)
                {
                    userRole.IsDeleted = true;
                }
                user.DeletedBy = _tokenHelper.GetUserIdFromContext();
                user.DeletedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }
    }
}
