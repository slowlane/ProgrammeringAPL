using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammeringAPL.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrepareForImgUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "GalleryImages",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "GalleryImages",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
