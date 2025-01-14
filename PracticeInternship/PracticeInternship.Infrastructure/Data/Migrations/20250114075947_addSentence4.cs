using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSentence4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DM_NCC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_NCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten_NCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_NCC", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DM_NCC");
        }
    }
}
