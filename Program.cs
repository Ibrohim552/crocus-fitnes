using CrocusFitnes;
using CrocusFitnes.Entities;
using CrocusFitnes.Extentions;
using GymSphere.Services.BookingService;
using GymSphere.Services.MemberService;
using GymSphere.Services.SessionService;
using GymSphere.Services.TrainerService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
