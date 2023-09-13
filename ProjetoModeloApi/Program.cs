using Microsoft.EntityFrameworkCore;
using ProjetoModelo.Data.Context;
using ProjetoModelo.Middleware;
using ProjetoModeloApi.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HPEContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Conn"));
    opt.UseLazyLoadingProxies();
});

builder.Services.StartRegisterServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS",
                      policy =>
                      {
                          policy.WithOrigins("*")
                                .WithHeaders("*")
                                .WithMethods("*");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
