using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesZoo.Migrations
{
    /// <inheritdoc />
    public partial class UserIDJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_journal_of_tasks_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "IdentityUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_journal_of_tasks_AspNetUsers_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                column: "IdentityUserID",
                principalSchema: "public",
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

            migrationBuilder.DropIndex(
                name: "IX_journal_of_tasks_IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks");

            migrationBuilder.DropColumn(
                name: "IdentityUserID",
                schema: "zoo_keepers",
                table: "journal_of_tasks");
        }
    }
}
