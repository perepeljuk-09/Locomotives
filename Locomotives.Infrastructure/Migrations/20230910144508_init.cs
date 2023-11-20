using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Locomotives.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "depots",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    depot_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "locomotive_categories",
                schema: "main",
                columns: table => new
                {
                    locomotive_category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locomotive_categories", x => x.locomotive_category_id);
                });

            migrationBuilder.CreateTable(
                name: "locomotives",
                schema: "main",
                columns: table => new
                {
                    locomotive_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    release_date = table.Column<DateOnly>(type: "date", nullable: false),
                    depot_id = table.Column<int>(type: "integer", nullable: false),
                    locomotive_category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locomotives", x => x.locomotive_id);
                    table.ForeignKey(
                        name: "FK_locomotives_depots_depot_id",
                        column: x => x.depot_id,
                        principalSchema: "main",
                        principalTable: "depots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locomotives_locomotive_categories_locomotive_category_id",
                        column: x => x.locomotive_category_id,
                        principalSchema: "main",
                        principalTable: "locomotive_categories",
                        principalColumn: "locomotive_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "drivers",
                schema: "main",
                columns: table => new
                {
                    driver_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    is_vacation = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    locomotive_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.driver_id);
                    table.ForeignKey(
                        name: "FK_drivers_locomotives_locomotive_id",
                        column: x => x.locomotive_id,
                        principalSchema: "main",
                        principalTable: "locomotives",
                        principalColumn: "locomotive_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "locomotive_categories_drivers",
                schema: "main",
                columns: table => new
                {
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    locomotive_category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locomotive_categories_drivers", x => new { x.locomotive_category_id, x.driver_id });
                    table.ForeignKey(
                        name: "FK_locomotive_categories_drivers_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "main",
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locomotive_categories_drivers_locomotive_categories_locomot~",
                        column: x => x.locomotive_category_id,
                        principalSchema: "main",
                        principalTable: "locomotive_categories",
                        principalColumn: "locomotive_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_drivers_locomotive_id",
                schema: "main",
                table: "drivers",
                column: "locomotive_id");

            migrationBuilder.CreateIndex(
                name: "IX_locomotive_categories_drivers_driver_id",
                schema: "main",
                table: "locomotive_categories_drivers",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_locomotives_depot_id",
                schema: "main",
                table: "locomotives",
                column: "depot_id");

            migrationBuilder.CreateIndex(
                name: "IX_locomotives_locomotive_category_id",
                schema: "main",
                table: "locomotives",
                column: "locomotive_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locomotive_categories_drivers",
                schema: "main");

            migrationBuilder.DropTable(
                name: "drivers",
                schema: "main");

            migrationBuilder.DropTable(
                name: "locomotives",
                schema: "main");

            migrationBuilder.DropTable(
                name: "depots",
                schema: "main");

            migrationBuilder.DropTable(
                name: "locomotive_categories",
                schema: "main");
        }
    }
}
