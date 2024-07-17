using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderManagementSystem.Application;
using OrderManagementSystem.Application.Helper;
using OrderManagementSystem.Application.Services.Auth;
using OrderManagementSystem.Domain.Entities.Identity;
using OrderManagementSystem.Infrastructure;
using OrderManagementSystem.Infrastructure.Databases;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
//Maping JWT Class With JWT in appsettings.json
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddControllers();

#region Identity
builder.Services.AddIdentity<User, IdentityRole>(o =>
{
    o.Password.RequiredLength = 8;
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;

    o.SignIn.RequireConfirmedAccount = false;
    o.SignIn.RequireConfirmedPhoneNumber = false;
    o.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>();
#endregion

#region Swagger Authorize configuration
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "’MissionSystemApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

});
#endregion

#region ConnectionString
builder.Services.AddDbContext<AppDbContext>(op =>
        op.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
#endregion

#region JWT Bearer Authorization
// add authentication services 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});
#endregion

#region Cors
var Cors = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(Cors,
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
#endregion


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddInfrastructureDependencies().AddApplicationDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region Seeding Admin & Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Customer" };
    //Create 
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            IdentityRole roleRole = new(role);
            await roleManager.CreateAsync(roleRole);
        }
    }
    // Create Admin
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string email = "minasamir9749@gmail.com";
    string password = "AdmIn%2024%";
    if (await userManager.FindByNameAsync(email) == null)
    {
        User user = new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Admin",
            NormalizedUserName = "Admin",
            Email = email,
            NormalizedEmail = email,
            SecurityStamp = string.Empty
        };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(Cors);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
