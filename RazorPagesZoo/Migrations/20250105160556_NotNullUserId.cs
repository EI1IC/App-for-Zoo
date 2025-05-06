using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesZoo.Migrations
{
    /// <inheritdoc />
    public partial class NotNullUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "IdentityUserId",
            schema: "zoo_keepers",
            table: "employee",
            type: "text",
            nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "IdentityUserId",
            schema: "zoo_keepers",
            table: "employee",
            type: "text",
            nullable: false);
        }
    }
}
