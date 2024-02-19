using CleanCodeTemplate.Business.Modules.TypeHandlers;
using CleanCodeTemplate.Business.Ports.Database.Input;
using CleanCodeTemplate.Business.Ports.Database.Output;
using CleanCodeTemplate.Business.Services;
using CleanCodeTemplate.Infrastructure.Adapters.Data;
using CleanCodeTemplate.Infrastructure.Adapters.Modules;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using CleanCodeTemplate.Infrastructure.Middlewares;
using Dapper;
using EasyCaching.SQLite;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

IDictionary<string, string> env = dotenv.net.DotEnv.Read();

SqlMapper.AddTypeHandler(typeof(IEnumerable<long>), new JsonListTypeHandler<long>());
SqlMapper.AddTypeHandler(typeof(IEnumerable<string>), new JsonListTypeHandler<string>());
SqlMapper.AddTypeHandler(new ByteArrayTypeHandler());
SqlMapper.AddTypeHandler(new JsonListTypeHandler<Guid>());
SqlMapper.AddTypeHandler(typeof(Guid), new GuidTypeHandler());

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints()
    .AddJWTBearerAuth(env["JWT_KEY"]) //add this
    .AddAuthorization()
    .SwaggerDocument();

builder.Services.AddSingleton(env);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMigrations();
builder.Services.AddDataAdapters();
builder.Services.AddTools();
builder.Services.AddServices();
builder.Services.AddPresenters();
builder.Services.AddEasyCaching(options =>
{
    options.UseInMemory("memory");
    options.UseSQLite(db => { db.DBConfig = new SQLiteDBOptions { FileName = "cache.db" }; }, name: "lite");
    options.UseSQLite(db => { db.DBConfig = new SQLiteDBOptions { FileName = "blocked.db" }; }, name: "blocked");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS",
        configure =>
        {
            configure.AllowAnyOrigin()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;

    var migrator = provider.GetRequiredService<MigratorContext>();

    migrator.Up();

    var databaseInitializerService = provider.GetRequiredService<IInitializeDatabaseInput>();
    var databaseInitializerPresenter = provider.GetRequiredService<IInitializeDatabaseOutput>();

    await databaseInitializerService.HandleAsync();

    Console.WriteLine(((IPresenter<string>)databaseInitializerPresenter).Response);
}


app.UseCors("CORS");
app.UseAuthentication() //add this
    .UseAuthorization() //add this
    .UseFastEndpoints()
    .UseSwaggerGen();

app.UseMiddleware<SessionMiddleware>();
app.UseMiddleware<BlockedMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<CatchMiddleware>();
app.Run();