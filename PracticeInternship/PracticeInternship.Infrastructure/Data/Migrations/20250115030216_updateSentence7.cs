using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateSentence7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "So_Phieu_Nhap_Kho",
                table: "DM_Nhap_Kho",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "So_Phieu_Nhap_Kho",
                table: "DM_Nhap_Kho");
        }
    }
}
