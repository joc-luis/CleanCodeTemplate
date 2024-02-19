using FluentMigrator;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Migrations;

[Migration(2024021502001)]
public class OptionMigration : Migration
{
    public override void Up()
    {
        Create.Table("Options")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("ParentId").AsGuid().Nullable()
            .WithColumn("Name").AsString().Unique()
            .WithColumn("Url").AsString().Unique()
            .WithColumn("Icon").AsString().Unique();
    }

    public override void Down()
    {
        Delete.Table("Options");
    }
}