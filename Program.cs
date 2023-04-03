using Centhora_Hotels.DB_Context;
using Centhora_Hotels.InternalServices.Calculate_Room_Price;
using Centhora_Hotels.InternalServices.Upload_Image_To_AWS_S3;
using Centhora_Hotels.Repository.Interface;
using Centhora_Hotels.Repository.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering the Db Context
builder.Services.AddDbContext<CenthoraDbContext>();

// Injecting repository and repository interface
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// Injecting the internal services
builder.Services.AddScoped<ICalculateRoomPrices, CalculateRoomPrices>();
builder.Services.AddScoped<IUploadImage, UploadImage>();

// Injecting and configuring AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Injecting the ILogger for loggings
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
