using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualPetBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPetSpriteSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "PetSprites",
                columns: new[] { "Id", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 1, "/images/rat1.png", "Default Rat" },
                    { 2, "/images/rat2.png", "Fancy Rat" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetSprites_SpriteId",
                table: "Pets",
                column: "SpriteId",
                principalTable: "PetSprites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetSprites_SpriteId",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetSprites",
                table: "PetSprites");

            migrationBuilder.DeleteData(
                table: "PetSprites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PetSprites",
                keyColumn: "Id",
                keyValue: 2);

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
    }
}
