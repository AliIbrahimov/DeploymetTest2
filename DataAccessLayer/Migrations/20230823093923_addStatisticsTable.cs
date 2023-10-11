using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addStatisticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaAccounts_Information_InfoId",
                table: "SocialMediaAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "InfoId",
                table: "SocialMediaAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 23, 9, 39, 23, 449, DateTimeKind.Utc).AddTicks(6719),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 19, 19, 22, 7, 551, DateTimeKind.Utc).AddTicks(6436));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 23, 13, 39, 23, 449, DateTimeKind.Local).AddTicks(5083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 19, 23, 22, 7, 550, DateTimeKind.Local).AddTicks(7510));

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<int>(type: "int", nullable: false),
                    ProjectsDone = table.Column<int>(type: "int", nullable: false),
                    WinAwards = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaAccounts_Information_InfoId",
                table: "SocialMediaAccounts",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaAccounts_Information_InfoId",
                table: "SocialMediaAccounts");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.AlterColumn<int>(
                name: "InfoId",
                table: "SocialMediaAccounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 19, 19, 22, 7, 551, DateTimeKind.Utc).AddTicks(6436),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 23, 9, 39, 23, 449, DateTimeKind.Utc).AddTicks(6719));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 19, 23, 22, 7, 550, DateTimeKind.Local).AddTicks(7510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 23, 13, 39, 23, 449, DateTimeKind.Local).AddTicks(5083));

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaAccounts_Information_InfoId",
                table: "SocialMediaAccounts",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id");
        }
    }
}
