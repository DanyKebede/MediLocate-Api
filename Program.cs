using mediAPI.Data;
using mediAPI.Extensions;
using mediAPI.Middleware;
using MediLast.Hubs;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseKestrel().UseUrls("http://localhost:5196", "https://localhost:7158", "http://0.0.0.0:5196");


builder.WebHost.UseKestrel().UseUrls("https://localhost:5196", "https://localhost:7158", "https://0.0.0.0:5196", "https://0.0.0.0:5565");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173",
                "https://localhost:5173",
                "https://localhost:5174",
                "http://localhost:5174",
                "https://10.161.68.103",
                "https://192.168.28.214",
                "http://192.168.28.214",
                "https://192.168.28.214:5174",
                "http://192.168.28.214:5174",
                "http://10.161.68.103")
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();


// application service configruation
builder.Services.AddApplicationServiceExtension(builder.Configuration);

// identity service configuration
builder.Services.AddIdentityServiceExtension(builder.Configuration);

// swagger service configuration

builder.Services.AddSwaggerServiceExtension(builder.Configuration);


builder.Services.AddTransient<Seed>();
// add signalR as a service
builder.Services.AddSignalR();

var app = builder.Build();



// Seed the database 
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//SeedData(app);

async void SeedData(IHost app)
{

    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        await service!.SeedMedicineAsync();
    }
}





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// add ExceptionMiddleware
app.UseExceptionMiddleware();

app.UseCors("AllowAll");
app.UseHttpsRedirection();

//app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
// add route for hubs
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");


app.Run();
