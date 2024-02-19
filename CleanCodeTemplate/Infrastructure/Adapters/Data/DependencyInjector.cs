using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Infrastructure.Adapters.Data.Migrations;
using CleanCodeTemplate.Infrastructure.Adapters.Data.Repositories;
using CleanCodeTemplate.Infrastructure.Enums;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data;

public static class DependencyInjector
{
    public static IServiceCollection AddMigrations(this IServiceCollection collection)
    {
        IDictionary<string, string> env = dotenv.net.DotEnv.Read();

        switch (env["DB_TYPE"])
        {
            case DbTypeEnum.Postgresql:
                NpgsqlConnectionStringBuilder npgsqlBuilder = new NpgsqlConnectionStringBuilder()
                {
                    Host = env["DB_HOST"],
                    Port = Convert.ToInt32(env["DB_PORT"]),
                    Database = env["DB_DATABASE"],
                    Username = env["DB_USER"],
                    Password = env["DB_PASSWORD"]
                };
                return collection.AddFluentMigratorCore()
                    .ConfigureRunner(rb =>
                        rb.AddPostgres()
                            .WithGlobalConnectionString(npgsqlBuilder.ConnectionString)
                            .ScanIn(typeof(BlockedMigration).Assembly)
                            .ScanIn(typeof(OptionMigration).Assembly)
                            .ScanIn(typeof(PermissionMigration).Assembly)
                            .ScanIn(typeof(RoleMigration).Assembly)
                            .ScanIn(typeof(UserMigration).Assembly)
                    ).AddLogging(lb => lb.AddFluentMigratorConsole());
            case DbTypeEnum.Mysql:
                MySqlConnectionStringBuilder mysqlBuilder = new MySqlConnectionStringBuilder()
                {
                    Server = env["DB_HOST"],
                    Port = Convert.ToUInt32(env["DB_PORT"]),
                    Database = env["DB_DATABASE"],
                    UserID = env["DB_USER"],
                    Password = env["DB_PASSWORD"]
                };
                return collection.AddFluentMigratorCore()
                    .ConfigureRunner(rb =>
                        rb.AddMySql8()
                            .WithGlobalConnectionString(mysqlBuilder.ConnectionString)
                            .ScanIn(typeof(BlockedMigration).Assembly)
                            .ScanIn(typeof(OptionMigration).Assembly)
                            .ScanIn(typeof(PermissionMigration).Assembly)
                            .ScanIn(typeof(RoleMigration).Assembly)
                            .ScanIn(typeof(UserMigration).Assembly)
                    ).AddLogging(lb => lb.AddFluentMigratorConsole());
            case DbTypeEnum.SqlServer:
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = env["DB_HOST"],
                    InitialCatalog = env["DB_DATABASE"],
                    UserID = env["DB_USER"],
                    Password = env["DB_PASSWORD"],
                    Encrypt = false
                };
                return collection.AddFluentMigratorCore()
                    .ConfigureRunner(rb =>
                        rb.AddSqlServer()
                            .WithGlobalConnectionString(sqlBuilder.ConnectionString)
                            .ScanIn(typeof(BlockedMigration).Assembly)
                            .ScanIn(typeof(OptionMigration).Assembly)
                            .ScanIn(typeof(PermissionMigration).Assembly)
                            .ScanIn(typeof(RoleMigration).Assembly)
                            .ScanIn(typeof(UserMigration).Assembly)
                    ).AddLogging(lb => lb.AddFluentMigratorConsole());
            case DbTypeEnum.Sqlite:
                SqliteConnectionStringBuilder sqliteBuilder = new SqliteConnectionStringBuilder()
                {
                    DataSource = env["DB_DATABASE"]
                };
                return collection.AddFluentMigratorCore()
                    .ConfigureRunner(rb =>
                        rb.AddSQLite()
                            .WithGlobalConnectionString(sqliteBuilder.ConnectionString)
                            .ScanIn(typeof(BlockedMigration).Assembly)
                            .ScanIn(typeof(OptionMigration).Assembly)
                            .ScanIn(typeof(PermissionMigration).Assembly)
                            .ScanIn(typeof(RoleMigration).Assembly)
                            .ScanIn(typeof(UserMigration).Assembly)
                    ).AddLogging(lb => lb.AddFluentMigratorConsole());
            default:
                throw new NotSupportedException("The selected database is not supported.");
        }
    }

    public static IServiceCollection AddDataAdapters(this IServiceCollection collection)
    {
        return collection.AddScoped<SqlKataContext>()
            .AddScoped<MigratorContext>()
            .AddTransient<IBlockedRepository, BlockedRepository>()
            .AddTransient<IOptionRepository, OptionRepository>()
            .AddTransient<IPermissionRepository, PermissionRepository>()
            .AddTransient<IRoleRepository, RoleRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IAccountRepository, AccountRepository>();
    }
}