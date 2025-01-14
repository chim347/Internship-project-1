using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSentence3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DM_San_Pham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_San_Pham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten_San_Pham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loai_San_Pham_Id = table.Column<int>(type: "int", nullable: false),
                    Don_Vi_Tinh_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_San_Pham", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DM_San_Pham");
        }
    }
}
