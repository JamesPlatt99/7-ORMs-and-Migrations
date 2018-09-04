using System;
using FluentMigrator;

namespace Migrations
{
    [Migration(20180409125943)]
    public class Test : Migration
    {
        public override void Up()
        {
            Create.Table("Hobby")
              .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
              .WithColumn("Name").AsString().NotNullable();

            Insert.IntoTable("Hobby")
              .Row(new { Name = "Fishing" })
              .Row(new { Name = "Drinking" });

            Create.Column("HobbyID").OnTable("Employees").AsInt32().Nullable()
              .ForeignKey("FK_Employees_Hobby", "Hobby", "Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Employees_Hobby").OnTable("Employees");
            Delete.Column("HobbyID").FromTable("Employees");
            Delete.Table("Hobby");
        }
    }
}
