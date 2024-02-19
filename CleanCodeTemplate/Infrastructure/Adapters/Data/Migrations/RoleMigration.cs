using FluentMigrator;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Migrations;

[Migration(2024021504)]
public class RoleMigration : Migration
{
    public override void Up()
    {
        Create.Table("Roles")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString().Unique()
            .WithColumn("Permissions").AsString(2147483647);
    }

    public override void Down()
    {
        Delete.Table("Roles");
    }
}