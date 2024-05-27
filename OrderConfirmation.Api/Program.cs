using OrderConfirmation.Api;
using OrderConfirmation.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddUserSecrets<Program>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailService>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.MapPost("/send-email", async (EmailRequest request, EmailService emailService) =>
{
    await emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Message);
    return Results.Ok("Email sent successfully.");
});

app.Run();