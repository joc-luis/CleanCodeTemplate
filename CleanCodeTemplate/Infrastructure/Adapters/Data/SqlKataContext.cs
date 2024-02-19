using CleanCodeTemplate.Infrastructure.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data;

public class SqlKataContext
{
    public QueryFactory QueryFactory { get; }

    public SqlKataContext(IDictionary<string, string> env)
    {
       
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
                QueryFactory = new QueryFactory(new NpgsqlConnection(npgsqlBuilder.ConnectionString), new PostgresCompiler());
                break;
            case DbTypeEnum.Mysql:
                MySqlConnectionStringBuilder mysqlBuilder = new MySqlConnectionStringBuilder()
                {
                    Server = env["DB_HOST"],
                    Port = Convert.ToUInt32(env["DB_PORT"]),
                    Database = env["DB_DATABASE"],
                    UserID = env["DB_USER"],
                    Password = env["DB_PASSWORD"]
                };
                QueryFactory = new QueryFactory(new MySqlConnection(mysqlBuilder.ConnectionString), new MySqlCompiler());
                // Log the compiled query to the console
                break;
            case DbTypeEnum.SqlServer:
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = env["DB_HOST"],
                    InitialCatalog = env["DB_DATABASE"],
                    UserID = env["DB_USER"],
                    Password = env["DB_PASSWORD"],
                    Encrypt = false
                };
                QueryFactory = new QueryFactory(new SqlConnection(sqlBuilder.ConnectionString), new SqlServerCompiler());
                break;
            case DbTypeEnum.Sqlite:
                SqliteConnectionStringBuilder sqliteBuilder = new SqliteConnectionStringBuilder()
                {
                    DataSource = env["DB_DATABASE"]
                };
                QueryFactory = new QueryFactory(new SqliteConnection(sqliteBuilder.ConnectionString), new SqliteCompiler());
                break;
            default:
                throw new NotSupportedException("The selected database is not supported.");
            
        }
        
        QueryFactory.Logger = compiled => {
            Console.WriteLine();
            Console.WriteLine(compiled.ToString());
            Console.WriteLine();
        };
    }
}