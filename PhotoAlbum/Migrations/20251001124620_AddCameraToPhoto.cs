using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoAlbum.Migrations
{
    /// <inheritdoc />
    public partial class AddCameraToPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Camera",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Camera",
                table: "Photo");
        }
    }
}
