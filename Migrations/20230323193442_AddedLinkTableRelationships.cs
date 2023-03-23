using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lagalt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedLinkTableRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Links",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Links_ProjectId",
                table: "Links",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Project_ProjectId",
                table: "Links",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Project_ProjectId",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_ProjectId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Links");
        }
    }
}
