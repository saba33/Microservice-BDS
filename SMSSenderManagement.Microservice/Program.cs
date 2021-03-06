using Microsoft.EntityFrameworkCore;
using SMSSenderManagement.Domain;
using SMSSenderManagement.Repository;
using SMSSenderManagement.Repository.Abstractions;
using SMSSenderManagement.Repository.Implementations;
using SMSSenderManagement.Repository.SmsManagementContect;
using SMSSenderManagement.Services.Abstractions;
using SMSSenderManagement.Services.Implemantations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
ConfigurationManager Configuration = builder.Configuration;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<ISmsRepository, SmsRepository>();
builder.Services.AddDbContext<SMSManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<ApiTokenInfo>(Configuration.GetSection(nameof(ApiTokenInfo)));

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
