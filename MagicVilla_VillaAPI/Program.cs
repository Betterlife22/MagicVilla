﻿


using MagicVilla_VillaAPI;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(option=> {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddAutoMapper(typeof(MappingConfig));
// configure log using serilog
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File
//    ("log/villalogs.txt", rollingInterval:RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
// Add services to the container.
// AddNewtonsoftJson để làm http patch
builder.Services.AddControllers().AddNewtonsoftJson();
//builder.Services.AddSingleton<ILogging,Logging>();
//AddXmlDataContractSerializerFormatters() option: thêm format xml cho web
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
