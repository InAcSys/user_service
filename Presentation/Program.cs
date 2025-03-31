using DotNetEnv;
using UserService.Presentation.Configuration;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

builder.WebHost.ConfigureKestrel(options => {
    options.ListenAnyIP(80);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguration();
builder.Services.AddControllers();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowLocalhost", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowLocalhost");
app.MapControllers();
app.Run();
