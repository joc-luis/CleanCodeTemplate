using FluentMigrator;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Migrations;

[Migration(2024021503)]
public class PermissionMigration : Migration
{
    public override void Up()
    {
        Create.Table("Permissions")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("OptionId").AsGuid().Nullable()
            .WithColumn("Name").AsString().Unique()
            .WithColumn("Url").AsString()
            .WithColumn("Method").AsString();
    }

    public override void Down()
    {
        Delete.Table("Permissions");
    }
}