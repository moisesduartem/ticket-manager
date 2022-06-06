using TicketManager.IoC.Extensions;
using TicketManager.Presentation.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureIoC(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options
    => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
        .WithOrigins(builder.Configuration.GetSection("Frontend:BaseUrl").Value)
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
