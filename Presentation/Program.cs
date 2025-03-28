using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowLocalhost", policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
    }
);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowLocalhost");

app.MapControllers();
app.Run();

