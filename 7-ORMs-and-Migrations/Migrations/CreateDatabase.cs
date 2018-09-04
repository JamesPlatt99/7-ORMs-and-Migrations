using System;
using FluentMigrator;

namespace Migrations
{
    [Migration(1)]
    public class CreateDatabase : Migration
    {
        public override void Up()
        {
            Create.Table("Employees")
              .WithColumn("Id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
              .WithColumn("FirstName").AsString().Nullable()
              .WithColumn("LastName").AsString().NotNullable()
              .WithColumn("Salary").AsDecimal().NotNullable()
              .WithColumn("Age").AsInt32().NotNullable();

            Create.Table("JobPosition")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString().NotNullable();

            Create.Column("JobPositionID").OnTable("Employees").AsInt32().NotNullable()
              .ForeignKey("FK_Employees_JobPosition", "JobPosition", "ID");

            Create.Table("PensionFund")
                .WithColumn("ID").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("ContributionAmount").AsDecimal().NotNullable()
                .WithColumn("LastContributionDate").AsDate().Nullable();

            Create.Column("PensionFundID").OnTable("Employees").AsGuid().Nullable()
              .ForeignKey("FK_Employees_PensionFund", "PensionFund", "ID");

            Create.Table("PensionFundProvider")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("IsDefault").AsBoolean().NotNullable();

            Create.Column("PensionFundProviderID").OnTable("PensionFund").AsInt32().NotNullable()
              .ForeignKey("FK_Employees_PensionFundProvider", "PensionFundProvider", "ID");
        }

        public override void Down()
        {
            Delete.Table("Employees");
            Delete.Table("JobPosition");
            Delete.Table("PensionFund");
            Delete.Table("PensionFundProvider");
        }
    }
}
