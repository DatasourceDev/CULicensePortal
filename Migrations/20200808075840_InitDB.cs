using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CULCS.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AzureGroups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AzureGroupId = table.Column<string>(nullable: true),
                    AzureGroupName = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Create_By = table.Column<string>(maxLength: 250, nullable: true),
                    Create_On = table.Column<DateTime>(nullable: true),
                    Update_By = table.Column<string>(maxLength: 250, nullable: true),
                    Update_On = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AzureGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageCode = table.Column<int>(nullable: false),
                    LanguageKey = table.Column<string>(nullable: false),
                    Page = table.Column<int>(nullable: false),
                    LanguageValue = table.Column<string>(nullable: false),
                    Create_By = table.Column<string>(maxLength: 250, nullable: true),
                    Create_On = table.Column<DateTime>(nullable: true),
                    Update_By = table.Column<string>(maxLength: 250, nullable: true),
                    Update_On = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceCode = table.Column<string>(nullable: true),
                    ProvinceName = table.Column<string>(nullable: true),
                    ProvinceNameEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceID);
                });

            migrationBuilder.CreateTable(
                name: "Setups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPageDesc = table.Column<string>(nullable: true),
                    AdminAzureGroupName = table.Column<string>(nullable: true),
                    PaymentFilter = table.Column<bool>(nullable: false),
                    Create_By = table.Column<string>(maxLength: 250, nullable: true),
                    Create_On = table.Column<DateTime>(nullable: true),
                    Update_By = table.Column<string>(maxLength: 250, nullable: true),
                    Update_On = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 250, nullable: false),
                    Password = table.Column<string>(maxLength: 250, nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    Create_By = table.Column<string>(maxLength: 250, nullable: true),
                    Create_On = table.Column<DateTime>(nullable: true),
                    Update_By = table.Column<string>(maxLength: 250, nullable: true),
                    Update_On = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProgramLicenses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AzureGroupID = table.Column<int>(nullable: false),
                    ProgramName = table.Column<string>(nullable: false),
                    ChulaDomain = table.Column<bool>(nullable: false),
                    StudentChulaDomain = table.Column<bool>(nullable: false),
                    AlumniChulaDomain = table.Column<bool>(nullable: false),
                    MaxBorrowAdvance = table.Column<int>(nullable: false),
                    MaxBorrow = table.Column<int>(nullable: false),
                    PeriodType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Create_By = table.Column<string>(maxLength: 250, nullable: true),
                    Create_On = table.Column<DateTime>(nullable: true),
                    Update_By = table.Column<string>(maxLength: 250, nullable: true),
                    Update_On = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramLicenses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramLicenses_AzureGroups_AzureGroupID",
                        column: x => x.AzureGroupID,
                        principalTable: "AzureGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aumphurs",
                columns: table => new
                {
                    AumphurID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AumphurCode = table.Column<string>(nullable: true),
                    AumphurName = table.Column<string>(nullable: true),
                    AumphurNameEn = table.Column<string>(nullable: true),
                    ProvinceCode = table.Column<string>(nullable: true),
                    ProvinceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aumphurs", x => x.AumphurID);
                    table.ForeignKey(
                        name: "FK_Aumphurs_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramLicenseID = table.Column<int>(nullable: false),
                    Domain = table.Column<string>(nullable: false),
                    BorrowDate = table.Column<DateTime>(nullable: true),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    BorrowStatus = table.Column<int>(nullable: false),
                    AzureUserId = table.Column<string>(nullable: true),
                    UserPrincipalName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Create_By = table.Column<string>(maxLength: 250, nullable: true),
                    Create_On = table.Column<DateTime>(nullable: true),
                    Update_By = table.Column<string>(maxLength: 250, nullable: true),
                    Update_On = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Borrows_ProgramLicenses_ProgramLicenseID",
                        column: x => x.ProgramLicenseID,
                        principalTable: "ProgramLicenses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tumbons",
                columns: table => new
                {
                    TumbonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TumbonCode = table.Column<string>(nullable: true),
                    TumbonName = table.Column<string>(nullable: true),
                    TumbonNameEn = table.Column<string>(nullable: true),
                    AumphurCode = table.Column<string>(nullable: true),
                    ProvinceCode = table.Column<string>(nullable: true),
                    ProvinceID = table.Column<int>(nullable: true),
                    AumphurID = table.Column<int>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tumbons", x => x.TumbonID);
                    table.ForeignKey(
                        name: "FK_Tumbons_Aumphurs_AumphurID",
                        column: x => x.AumphurID,
                        principalTable: "Aumphurs",
                        principalColumn: "AumphurID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tumbons_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aumphurs_ProvinceID",
                table: "Aumphurs",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_ProgramLicenseID",
                table: "Borrows",
                column: "ProgramLicenseID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLicenses_AzureGroupID",
                table: "ProgramLicenses",
                column: "AzureGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Tumbons_AumphurID",
                table: "Tumbons",
                column: "AumphurID");

            migrationBuilder.CreateIndex(
                name: "IX_Tumbons_ProvinceID",
                table: "Tumbons",
                column: "ProvinceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrows");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Setups");

            migrationBuilder.DropTable(
                name: "Tumbons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProgramLicenses");

            migrationBuilder.DropTable(
                name: "Aumphurs");

            migrationBuilder.DropTable(
                name: "AzureGroups");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
