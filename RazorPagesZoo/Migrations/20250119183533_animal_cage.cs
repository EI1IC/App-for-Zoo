using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesZoo.Migrations
{
    /// <inheritdoc />
    public partial class animal_cage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cage_animal",
                schema: "zoo_keepers");

            migrationBuilder.DropTable(
                name: "compatibility",
                schema: "zoo_keepers");

            migrationBuilder.AddColumn<int>(
                name: "AnimalIdAnimal",
                schema: "zoo_keepers",
                table: "cage",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_cage",
                schema: "zoo_keepers",
                table: "animal",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cage_AnimalIdAnimal",
                schema: "zoo_keepers",
                table: "cage",
                column: "AnimalIdAnimal");

            migrationBuilder.CreateIndex(
                name: "IX_animal_id_cage",
                schema: "zoo_keepers",
                table: "animal",
                column: "id_cage");

            migrationBuilder.AddForeignKey(
                name: "fk_animal_cage",
                schema: "zoo_keepers",
                table: "animal",
                column: "id_cage",
                principalSchema: "zoo_keepers",
                principalTable: "cage",
                principalColumn: "id_cage",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_cage_animal_AnimalIdAnimal",
                schema: "zoo_keepers",
                table: "cage",
                column: "AnimalIdAnimal",
                principalSchema: "zoo_keepers",
                principalTable: "animal",
                principalColumn: "id_animal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_animal_cage",
                schema: "zoo_keepers",
                table: "animal");

            migrationBuilder.DropForeignKey(
                name: "FK_cage_animal_AnimalIdAnimal",
                schema: "zoo_keepers",
                table: "cage");

            migrationBuilder.DropIndex(
                name: "IX_cage_AnimalIdAnimal",
                schema: "zoo_keepers",
                table: "cage");

            migrationBuilder.DropIndex(
                name: "IX_animal_id_cage",
                schema: "zoo_keepers",
                table: "animal");

            migrationBuilder.DropColumn(
                name: "AnimalIdAnimal",
                schema: "zoo_keepers",
                table: "cage");

            migrationBuilder.DropColumn(
                name: "id_cage",
                schema: "zoo_keepers",
                table: "animal");

            migrationBuilder.CreateTable(
                name: "cage_animal",
                schema: "zoo_keepers",
                columns: table => new
                {
                    id_cage = table.Column<int>(type: "integer", nullable: false),
                    id_animal = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cage_cage_animalpk", x => new { x.id_cage, x.id_animal });
                    table.ForeignKey(
                        name: "cage_animalfk",
                        column: x => x.id_animal,
                        principalSchema: "zoo_keepers",
                        principalTable: "animal",
                        principalColumn: "id_animal");
                    table.ForeignKey(
                        name: "cage_cagefk",
                        column: x => x.id_cage,
                        principalSchema: "zoo_keepers",
                        principalTable: "cage",
                        principalColumn: "id_cage");
                });

            migrationBuilder.CreateTable(
                name: "compatibility",
                schema: "zoo_keepers",
                columns: table => new
                {
                    id_species_1 = table.Column<int>(type: "integer", nullable: false),
                    id_species_2 = table.Column<int>(type: "integer", nullable: false),
                    id_cage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("compatibilitypk", x => new { x.id_species_1, x.id_species_2, x.id_cage });
                    table.ForeignKey(
                        name: "compatibility_cagefk",
                        column: x => x.id_cage,
                        principalSchema: "zoo_keepers",
                        principalTable: "cage",
                        principalColumn: "id_cage");
                    table.ForeignKey(
                        name: "compatibility_id1fk",
                        column: x => x.id_species_1,
                        principalSchema: "zoo_keepers",
                        principalTable: "species_note",
                        principalColumn: "id_species");
                    table.ForeignKey(
                        name: "compatibility_id2fk",
                        column: x => x.id_species_2,
                        principalSchema: "zoo_keepers",
                        principalTable: "species_note",
                        principalColumn: "id_species");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cage_animal_id_animal",
                schema: "zoo_keepers",
                table: "cage_animal",
                column: "id_animal");

            migrationBuilder.CreateIndex(
                name: "IX_compatibility_id_cage",
                schema: "zoo_keepers",
                table: "compatibility",
                column: "id_cage");

            migrationBuilder.CreateIndex(
                name: "IX_compatibility_id_species_2",
                schema: "zoo_keepers",
                table: "compatibility",
                column: "id_species_2");
        }
    }
}
