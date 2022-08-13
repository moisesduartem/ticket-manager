using Serilog;
using TicketManager.Api.Infra.IoC;
using TicketManager.Api.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) => 
    cfg.WriteTo.Console(outputTemplate: "[{Timestamp:o} {Level:u3}] => {Message:lj} {Properties:j} {NewLine} {Exception}")
);

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
