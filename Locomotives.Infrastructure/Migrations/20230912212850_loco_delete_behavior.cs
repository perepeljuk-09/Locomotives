using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class loco_delete_behavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_locomotives_locomotive_id",
                schema: "main",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_locomotives_depots_depot_id",
                schema: "main",
                table: "locomotives");

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_locomotives_locomotive_id",
                schema: "main",
                table: "drivers",
                column: "locomotive_id",
                principalSchema: "main",
                principalTable: "locomotives",
                principalColumn: "locomotive_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_locomotives_depots_depot_id",
                schema: "main",
                table: "locomotives",
                column: "depot_id",
                principalSchema: "main",
                principalTable: "depots",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_locomotives_locomotive_id",
                schema: "main",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_locomotives_depots_depot_id",
                schema: "main",
                table: "locomotives");

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_locomotives_locomotive_id",
                schema: "main",
                table: "drivers",
                column: "locomotive_id",
                principalSchema: "main",
                principalTable: "locomotives",
                principalColumn: "locomotive_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_locomotives_depots_depot_id",
                schema: "main",
                table: "locomotives",
                column: "depot_id",
                principalSchema: "main",
                principalTable: "depots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
