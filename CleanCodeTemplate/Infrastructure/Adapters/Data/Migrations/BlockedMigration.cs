using FluentMigrator;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Migrations;

[Migration(2024021501)]
public class BlockedMigration : Migration
{
    public override void Up()
    {
        Create.Table("Blocked")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("UserId").AsGuid()
            .WithColumn("UserBlockedId").AsGuid()
            .WithColumn("Description").AsString()
            .WithColumn("Start").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("End").AsDateTime();
    }

    public override void Down()
    {
        Delete.Table("Blocked");
    }
}