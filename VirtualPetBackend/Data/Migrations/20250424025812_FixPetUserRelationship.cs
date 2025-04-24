using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualPetBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPetUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pets_PetId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PetId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_UserId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PetId",
                table: "Users",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pets_PetId",
                table: "Users",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id");
        }
    }
}
