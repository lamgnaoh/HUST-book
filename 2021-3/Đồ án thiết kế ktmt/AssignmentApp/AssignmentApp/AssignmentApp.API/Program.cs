using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using AssignmentApp.API.Repository.Assignments;
using AssignmentApp.API.Repository.Classes;
using AssignmentApp.API.Repository.Files;
using AssignmentApp.API.Repository.StudentAssignment;
using AssignmentApp.API.Repository.Token;
using AssignmentApp.API.Repository.UserRoles;
using AssignmentApp.API.Repository.Users;
using AssignmentApp.Data.EF;
using AssignmentApp.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TokenHandler = AssignmentApp.API.Repository.Token.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// chay tren chrome
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
    }));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme()
    {
        Name = "JWT Authentication",
        Description = "Enter a valid jwt bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
        
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id,securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement() 
        {
            {securityScheme , new string[] {}}
        }
    );
});

// DI
var connectionString = builder.Configuration.GetConnectionString("AssignmentAppDatabase");
builder.Services.AddDbContext<AssignmentAppDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IStudentAssignmentRepository, StudentAssignmentRepository>();
builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

// builder.Services.AddTransient<UserManager<User>, UserManager<User>>();
// builder.Services.AddTransient<SignInManager<User>, SignInManager<User>>();
// builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
// builder.Services.AddIdentity<User, AppRole>().AddEntityFrameworkStores<AssignmentAppDbContext>()
//     .AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });
// builder.Services.Configure<IdentityOptions>(options =>
//     options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();