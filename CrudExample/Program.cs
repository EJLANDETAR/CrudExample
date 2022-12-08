using CrudExample;
using CrudExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApplicationOptions>(opts => builder.Configuration.GetSection("ApplicationOptions").Bind(opts));

// Add Cors
builder.Services.AddCustomCors(builder.Configuration, "CorsPolicySpecificHosts");

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicySpecificHosts");

app.UseHttpsRedirection();

app.UseAuthorization();

//Middleware check api key
app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
