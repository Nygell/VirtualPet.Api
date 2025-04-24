using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualPetBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPlease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetSprites_SpriteId",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetSprites",
                table: "PetSprites");

            migrationBuilder.RenameTable(
                name: "PetSprites",
                newName: "PetSprite");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetSprite",
                table: "PetSprite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetSprite_SpriteId",
                table: "Pets",
                column: "SpriteId",
                principalTable: "PetSprite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetSprite_SpriteId",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetSprite",
                table: "PetSprite");

            migrationBuilder.RenameTable(
                name: "PetSprite",
                newName: "PetSprites");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetSprites",
                table: "PetSprites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetSprites_SpriteId",
                table: "Pets",
                column: "SpriteId",
                principalTable: "PetSprites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
