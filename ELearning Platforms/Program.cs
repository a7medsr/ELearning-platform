using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


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
