using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesZoo.Migrations
{
    /// <inheritdoc />
    public partial class identityJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_journal_of_tasks_UserId",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_UserId",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_UserId",
                schema: "zoo_keepers",
                table: "journal_of_tasks");

            migrationBuilder.DropIndex(
                name: "IX_journal_of_tasks_UserId",
                schema: "zoo_keepers",
                table: "journal_of_tasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "zoo_keepers",
                table: "journal_of_tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
