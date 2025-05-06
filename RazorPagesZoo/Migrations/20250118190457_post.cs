using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesZoo.Migrations
{
    /// <inheritdoc />
    public partial class post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropIndex(
                name: "IX_employee_id_post",
                schema: "zoo_keepers",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "id_post",
                schema: "zoo_keepers",
                table: "employee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_post",
                schema: "zoo_keepers",
                table: "employee",
                type: "integer",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "post",
                schema: "zoo_keepers",
                columns: table => new
                {
                    id_post = table.Column<int>(type: "integer", nullable: false),
                    charge = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id_post_pkey", x => x.id_post);
                });

            migrationBuilder.CreateIndex(
                name: "IX_journal_of_tasks_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "IdentityUserID");

            migrationBuilder.CreateIndex(
                name: "IX_employee_id_post",
                schema: "zoo_keepers",
                table: "employee",
                column: "id_post");

            migrationBuilder.AddForeignKey(
                name: "employee_postfk",
                schema: "zoo_keepers",
                table: "employee",
                column: "id_post",
                principalSchema: "zoo_keepers",
                principalTable: "post",
                principalColumn: "id_post");

            migrationBuilder.AddForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
