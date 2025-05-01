using ELearning_Platforms.Application.Mappings;
using ELearning_Platforms.Application.Services;
using ELearning_Platforms.Application.ServicesInterfaces;
using ELearning_Platforms.Domain.Interfaces;
using ELearning_Platforms.Infrastructure.Repositories;
using ELearning_Platforms.Models;
using ELearning_Platforms.Models.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ELearningDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddIdentity<BaseUser, IdentityRole>(options =>
{
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
})
                .AddEntityFrameworkStores<ELearningDbContext>()
                .AddDefaultTokenProviders();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = new[] { "Student", "Teacher" };

    foreach (var role in roles)
    {
        var exists = await roleManager.RoleExistsAsync(role);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.UseSwagger(opt =>
{
    opt.RouteTemplate = "openapi/{documentName}.json";
});
app.MapScalarApiReference(opt =>
{
    opt.Title = "Scalar API Reference";
    opt.Theme = ScalarTheme.Mars;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
