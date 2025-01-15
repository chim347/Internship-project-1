using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSentence8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XNK_Nhap_Kho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nhap_Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    So_Phieu_Nhap_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NCC_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ngay_Nhap_kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XNK_Nhap_Kho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XNK_Nhap_Kho_DM_Nhap_Kho_Nhap_Kho_Id",
                        column: x => x.Nhap_Kho_Id,
                        principalTable: "DM_Nhap_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_XNK_Nhap_Kho_Nhap_Kho_Id",
                table: "XNK_Nhap_Kho",
                column: "Nhap_Kho_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XNK_Nhap_Kho");
        }
    }
}
