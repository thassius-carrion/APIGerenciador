using AutoMapper;
using Manager.API.ViewModel;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Service.DTO;
using Manager.Service.Interfaces;
using Manager.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.

var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserDTO>().ReverseMap();
    cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
    cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

builder.Services.AddDbContext<ManagerContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:USER_MANAGER"]), ServiceLifetime.Transient);

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
