using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManager.Infra.Migrations
{
    public partial class Category_Tickets_Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9092), "Notebook" },
                    { 2, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9094), "Network" },
                    { 3, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9094), "PC" },
                    { 4, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9095), "Printer" },
                    { 5, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9096), "Other" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(8958));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedAt", "Description", "IsSolved", "Title" },
                values: new object[] { 1, 1, 1, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9108), "I don't what happened, but it started to flame from nothing", false, "My notebook is on fire!" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedAt", "Description", "IsSolved", "Title" },
                values: new object[] { 2, 1, 2, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9110), "", false, "I'm without internet" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedAt", "Description", "IsSolved", "Title" },
                values: new object[] { 3, 2, 4, new DateTime(2022, 6, 5, 15, 14, 14, 804, DateTimeKind.Local).AddTicks(9111), "", false, "My printer isn't working" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 11, 4, 55, 187, DateTimeKind.Local).AddTicks(7356));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 5, 22, 11, 4, 55, 187, DateTimeKind.Local).AddTicks(7365));
        }
    }
}
