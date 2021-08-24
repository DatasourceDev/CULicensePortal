using Microsoft.EntityFrameworkCore.Migrations;

namespace CULCS.Migrations
{
    public partial class AlterAzureGroupTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TempAzureGroupId",
                table: "AzureGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TempAzureGroupName",
                table: "AzureGroups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempAzureGroupId",
                table: "AzureGroups");

            migrationBuilder.DropColumn(
                name: "TempAzureGroupName",
                table: "AzureGroups");
        }
    }
}
