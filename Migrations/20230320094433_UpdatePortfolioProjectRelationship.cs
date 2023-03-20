using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lagalt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePortfolioProjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_PortfolioId",
                table: "Project",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Portfolio_PortfolioId",
                table: "Project",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Portfolio_PortfolioId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_PortfolioId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Project");
        }
    }
}
