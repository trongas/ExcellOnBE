//using API.Hubs;
using API.Hubs;
using Application;
using Infrastructure;
using Infrastructure.Config;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInFrastructure(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddSingleton<ChatService>();
builder.Services.AddScoped<ChatHub>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});


builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("MailSetting"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddLogging();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
    AuthSeedData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("chat");

app.UseCors(MyAllowSpecificOrigins);

app.Run();
