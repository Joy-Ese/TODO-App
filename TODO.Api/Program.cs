using MongoDB.Driver;
using NLog;
using NLog.Web;
using TODO.Services.Interfaces;
using TODO.Services.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // ---------------- Start MongoDB integration ---------------------------------------------------------------------------------------------
    builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
    string connectionString = config.GetConnectionString("MongoDBConnection");

    var settings = MongoClientSettings.FromConnectionString(connectionString);

    // Set the ServerApi field of the settings object to set the version of the Stable API on the client
    settings.ServerApi = new ServerApi(ServerApiVersion.V1);

    // Create a new client and connect to the server
    var client = new MongoClient(settings);

    // Send a ping to confirm a successful connection
    try
    {
        // Check if the database exists and Get the list of databases
        var database = client.GetDatabase("ToDoAppDB");

        var databases = client.ListDatabaseNames().ToList();
        if (!databases.Contains("ToDoAppDB"))
        {
            // Create the database if it doesn't exist
            client.GetDatabase("ToDoAppDB").CreateCollection("User");
        }

        Console.WriteLine("You successfully connected to MongoDB and created a database!");

        foreach (string db in databases)
        {
            Console.WriteLine(db);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
    // ---------------- End MongoDB integration ---------------------------------------------------------------------------------------------


    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddControllers();

    // Add services to the container.
    builder.Services.AddScoped<IAuth, AuthServices>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/ threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
