using OrderConfirmation.Api;
using OrderConfirmation.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddUserSecrets<Program>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/send-email", async (EmailRequest request, EmailService emailService) =>
{
    try
    {
        await emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Message);
        return Results.Ok("Email sent successfully.");
    }
    catch (Exception e)
    {
        return Results.BadRequest(e);
    }
});

app.Run();