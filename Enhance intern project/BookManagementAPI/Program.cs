using BookManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register BookService as a singleton
builder.Services.AddSingleton<BookService>();

// Configure CORS to allow Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Ensure the frontend URL is correct
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configure Kestrel to listen on both HTTP and HTTPS
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5000); // Listen on HTTP (port 5000)
    serverOptions.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps(); // Listen on HTTPS (port 5001)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS

// Enable CORS
app.UseCors("AllowAngularApp");

// Enable routing and authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Start the application
app.Run();
