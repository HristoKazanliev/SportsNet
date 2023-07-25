using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsNet.Data.Migrations
{
    public partial class SeededPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DeletedOn", "IsDeleted", "ModifiedOn", "Title", "Type" },
                values: new object[] { new Guid("63066625-0922-42f3-8798-f132295079e3"), new Guid("9e0898d3-b83d-4583-b356-9d0c363eb67c"), 3, "The impact of Norris' traditional celebration of smashing the champagne bottle on the ground to spray the champagne accidentally saw Verstappen's trophy fall over and break.", null, false, null, "Lando breaks Max's trophy!", 5 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DeletedOn", "IsDeleted", "ModifiedOn", "Title", "Type" },
                values: new object[] { new Guid("9adecfec-0a09-4aca-9738-7aa9e4f478d0"), new Guid("9e0898d3-b83d-4583-b356-9d0c363eb67c"), 1, "Paris Saint-Germain have granted permission for Kylian Mbappe to speak to Al Hilal after the Saudi club's world-record £259m bid.", null, false, null, "Kylian Mbappe: PSG grant forward permission to speak to Al Hilal", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DeletedOn", "IsDeleted", "ModifiedOn", "Title", "Type" },
                values: new object[] { new Guid("a3befd9b-17f7-4770-8b57-cdb89441b3e0"), new Guid("9e0898d3-b83d-4583-b356-9d0c363eb67c"), 2, "World No 1 Carlos Alcaraz ended Novak Djokovic's hopes of a record-equalling 24th Grand Slam to claim his maiden Wimbledon title in a five-set epic, 1-6 7-6 (8-6) 6-1 3-6 6-4.", null, false, null, "Wimbledon men's final: Carlos Alcaraz defeats seven-time champion Novak Djokovic", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("63066625-0922-42f3-8798-f132295079e3"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("9adecfec-0a09-4aca-9738-7aa9e4f478d0"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("a3befd9b-17f7-4770-8b57-cdb89441b3e0"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
