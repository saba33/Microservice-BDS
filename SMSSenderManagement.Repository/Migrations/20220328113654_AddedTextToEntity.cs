using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSSenderManagement.Repository.Migrations
{
    public partial class AddedTextToEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "OtpAndIds",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "OtpAndIds");
        }
    }
}
