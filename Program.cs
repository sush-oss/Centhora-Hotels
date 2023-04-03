using Centhora_Hotels.DB_Context;
using Centhora_Hotels.InternalServices.Calculate_Room_Price;
using Centhora_Hotels.InternalServices.CenthoraAuth;
using Centhora_Hotels.InternalServices.Upload_Image_To_AWS_S3;
using Centhora_Hotels.Repository.Interface;
using Centhora_Hotels.Repository.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<ICenthoraAuth,  CenthoraAuth>();

// Injecting and configuring AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Injecting the ILogger for loggings
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// JWT Autentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"])),
        ValidIssuer = builder.Configuration["JWT:Issure"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
    };
});

// JWT Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
