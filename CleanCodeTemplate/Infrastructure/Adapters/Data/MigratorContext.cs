using FluentMigrator.Runner;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data;

public class MigratorContext
{
    private readonly IMigrationRunner _migrationRunner;
    
    
    public MigratorContext(IMigrationRunner migrationRunner)
    {
        _migrationRunner = migrationRunner;
    }

    public void Up()
    {
        _migrationRunner.MigrateUp();
    }

    public void Down()
    {
        throw new NotImplementedException();
    }
    
}