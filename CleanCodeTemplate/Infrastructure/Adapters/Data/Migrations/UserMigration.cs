using FluentMigrator;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Migrations;

[Migration(2024021505)]
public class UserMigration : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("RoleId").AsGuid()
            .WithColumn("Nick").AsString().Unique()
            .WithColumn("Email").AsString().Unique()
            .WithColumn("Password").AsString().Unique()
            .WithColumn("Image").AsBinary()
            .WithColumn("TwoFactors").AsBoolean();
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}