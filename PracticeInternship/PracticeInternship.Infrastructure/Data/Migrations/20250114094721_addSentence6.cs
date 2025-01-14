using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSentence6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DM_Kho_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_Dang_Nhap = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Kho_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Kho_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DM_Kho_User_Ma_Dang_Nhap_Kho_Id",
                table: "DM_Kho_User",
                columns: new[] { "Ma_Dang_Nhap", "Kho_Id" },
                unique: true,
                filter: "[Ma_Dang_Nhap] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DM_Kho_User");
        }
    }
}
