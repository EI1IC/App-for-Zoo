using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RazorPagesZoo.Migrations
{
    /// <inheritdoc />
    public partial class employeeIdLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "id_employee",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                type: "serial",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "id_employee",
                schema: "zoo_keepers",
                table: "employee",
                type: "serial",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "zoo_keepers",
                table: "employee",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "zoo_keepers",
                table: "employee");

            migrationBuilder.AlterColumn<int>(
                name: "id_employee",
                schema: "zoo_keepers",
                table: "journal_of_tasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "serial");

            migrationBuilder.AlterColumn<int>(
                name: "id_employee",
                schema: "zoo_keepers",
                table: "employee",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "serial")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);
        }
    }
}
