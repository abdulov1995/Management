using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using Management.Auth;
using Management.Auth.Validators;
using Management.Extentions.TokenHelper;
using Management.Roles;
using Management.Roles.Dto;
using Management.Users;
using Management.Users.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentWebApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Management", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \" Bearer <token>\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAutoMapper(typeof(UserMapper).Assembly);
builder.Services.AddAutoMapper(typeof(RoleMapper).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<EmailValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PasswordValidator>();
//builder.Services.AddControllers().AddFluentValidation(fv =>
//                fv.RegisterValidatorsFromAssemblyContaining<EmailValidator>());
//builder.Services.AddControllers().AddFluentValidation(fv =>
//                fv.RegisterValidatorsFromAssemblyContaining<PasswordValidator>());
builder.Services.AddControllers().AddFluentValidation(fv =>
               {
                   fv.RegisterValidatorsFromAssemblyContaining<UserCreateDto>();
                   fv.RegisterValidatorsFromAssemblyContaining<RoleCreateDto>();
                   fv.RegisterValidatorsFromAssemblyContaining<UserUpdateDto>();
                   fv.RegisterValidatorsFromAssemblyContaining<RoleUpdateDto>();
               });


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenHelper>();
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