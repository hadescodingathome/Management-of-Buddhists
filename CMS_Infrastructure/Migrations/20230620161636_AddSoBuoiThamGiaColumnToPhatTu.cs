using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_Infrastructure.Migrations
{
    public partial class AddSoBuoiThamGiaColumnToPhatTu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoBuoiThamGia",
                table: "PhatTu",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoBuoiThamGia",
                table: "PhatTu");
        }
    }
}
