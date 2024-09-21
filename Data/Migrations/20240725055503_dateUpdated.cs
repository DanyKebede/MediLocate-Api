using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLast.Data.Migrations
{
    public partial class dateUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11356557-ac3a-4f69-ab8f-71c5e6d31bda"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d115af34-ce82-48ab-bd63-5e307c3210eb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e72fa451-1226-42a1-813a-67c7fb9e6e78"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("81505539-e039-4172-b83d-2eca887113b6"), "7790b457-583f-4e9a-abbd-52ec73f46dd4", "Admin", "ADMIN" },
                    { new Guid("de39ce07-5e86-4182-820e-18bfe87f0b16"), "5fd04f0e-219a-4036-a8b9-6571493b79f1", "Pharmacy", "PHARMACY" },
                    { new Guid("fb76720a-42b6-42d7-8c52-ce0028e49384"), "620e6db6-1250-451b-a86c-b205a4acaa33", "Customer", "CUSTOMER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("81505539-e039-4172-b83d-2eca887113b6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("de39ce07-5e86-4182-820e-18bfe87f0b16"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fb76720a-42b6-42d7-8c52-ce0028e49384"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11356557-ac3a-4f69-ab8f-71c5e6d31bda"), "e54601f6-c714-499d-a3cd-cccdd2f491a7", "Customer", "CUSTOMER" },
                    { new Guid("d115af34-ce82-48ab-bd63-5e307c3210eb"), "a3f37d5a-7843-49da-bd01-16e58512a17d", "Admin", "ADMIN" },
                    { new Guid("e72fa451-1226-42a1-813a-67c7fb9e6e78"), "f03f248a-80d5-4116-b633-6598f59ac01d", "Pharmacy", "PHARMACY" }
                });
        }
    }
}
