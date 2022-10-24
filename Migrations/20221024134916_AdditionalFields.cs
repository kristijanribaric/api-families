using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamiliesApi.Migrations
{
    public partial class AdditionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteCar",
                table: "FamilyMembers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavoriteDish",
                table: "FamilyMembers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavoriteToy",
                table: "FamilyMembers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteCar",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "FavoriteDish",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "FavoriteToy",
                table: "FamilyMembers");
        }
    }
}
