using FluentValidation.AspNetCore;
using Management.Roles;
using Management.Roles.Dto;
using Management.Users;
using Management.Users.Dto;
using Microsoft.EntityFrameworkCore;
using StudentWebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);
builder.Services.AddControllers().AddFluentValidation(fv =>
               { fv.RegisterValidatorsFromAssemblyContaining<UserCreateDto>();
                 fv.RegisterValidatorsFromAssemblyContaining<RoleCreateDto>();
               });


builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();