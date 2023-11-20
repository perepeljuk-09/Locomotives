using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class delete_loco_required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "main",
                table: "depots",
                newName: "depot_id");

            migrationBuilder.AlterColumn<int>(
                name: "depot_id",
                schema: "main",
                table: "locomotives",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "depot_id",
                schema: "main",
                table: "depots",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "depot_id",
                schema: "main",
                table: "locomotives",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
