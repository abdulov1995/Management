using FluentValidation.AspNetCore;
using Management.Auth;
using Management.Roles;
using Management.Roles.Dto;
using Management.Users;
using Management.Users.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentWebApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IPasswordHasher<SignIn>, PasswordHasher<SignIn>>();
builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);
builder.Services.AddAutoMapper(typeof(RoleMapper).Assembly);
builder.Services.AddControllers().AddFluentValidation(fv =>
               {
                   fv.RegisterValidatorsFromAssemblyContaining<UserCreateDto>();
                   fv.RegisterValidatorsFromAssemblyContaining<RoleCreateDto>();
                   fv.RegisterValidatorsFromAssemblyContaining<UserUpdateDto>();
                   fv.RegisterValidatorsFromAssemblyContaining<RoleUpdateDto>();
               });


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtKey = builder.Configuration["Jwt:Key"];
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];
    var jwtAudience = builder.Configuration["Jwt:Audience"];

    if (string.IsNullOrWhiteSpace(jwtKey) || string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
    {
        throw new Exception("JWT configuration values are not set properly.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();