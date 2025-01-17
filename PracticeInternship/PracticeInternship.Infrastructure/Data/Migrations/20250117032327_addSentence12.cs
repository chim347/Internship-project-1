using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSentence12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XNK_Xuat_Kho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Xuat_Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    So_Phieu_Xuat_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ngay_Xuat_kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XNK_Xuat_Kho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XNK_Xuat_Kho_DM_Xuat_Kho_Xuat_Kho_Id",
                        column: x => x.Xuat_Kho_Id,
                        principalTable: "DM_Xuat_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_XNK_Xuat_Kho_Xuat_Kho_Id",
                table: "XNK_Xuat_Kho",
                column: "Xuat_Kho_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XNK_Xuat_Kho");
        }
    }
}
