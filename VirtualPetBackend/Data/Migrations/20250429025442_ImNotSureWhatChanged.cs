using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualPetBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImNotSureWhatChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetSpriteId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetSpriteId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
