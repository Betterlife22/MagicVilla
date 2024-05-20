using MagicVilla_VillaAPI.Logging;


var builder = WebApplication.CreateBuilder(args);
// configure log using serilog
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File
//    ("log/villalogs.txt", rollingInterval:RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
// Add services to the container.
// AddNewtonsoftJson để làm http patch
builder.Services.AddControllers(option=>
option.ReturnHttpNotAcceptable=true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
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
