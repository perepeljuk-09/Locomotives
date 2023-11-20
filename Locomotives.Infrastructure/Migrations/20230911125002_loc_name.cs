using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class loc_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "locomotive_name",
                schema: "main",
                table: "locomotives",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                schema: "main",
                table: "locomotive_categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "locomotive_name",
                schema: "main",
                table: "locomotives");

            migrationBuilder.AlterColumn<int>(
                name: "category_name",
                schema: "main",
                table: "locomotive_categories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
