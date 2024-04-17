using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_opendata.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRepository : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepositoryType",
                table: "Repository",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepositoryType",
                table: "Repository");
        }
    }
}
