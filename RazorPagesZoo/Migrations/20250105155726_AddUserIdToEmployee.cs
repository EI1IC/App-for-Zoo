using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesZoo.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "IdentityUserId",
            schema: "zoo_keepers",
            table: "employee",
            type: "text",
            nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IdentityUserId",
                schema: "zoo_keepers",
                table: "employee",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_IdentityUserId",
                schema: "zoo_keepers",
                table: "employee",
                column: "IdentityUserId",
                principalSchema: "public", 
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_Employee_AspNetUsers_IdentityUserId",
            schema: "zoo_keepers",
            table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_IdentityUserId",
                schema: "zoo_keepers",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                schema: "zoo_keepers",
                table: "employee");
        }
    }
}
