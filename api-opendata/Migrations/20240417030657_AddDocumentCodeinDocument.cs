using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_opendata.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentCodeinDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentCode",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentCode",
                table: "Document");
        }
    }
}
