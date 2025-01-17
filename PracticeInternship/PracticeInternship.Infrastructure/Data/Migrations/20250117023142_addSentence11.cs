using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSentence11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DM_Xuat_Kho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    So_Phieu_Xuat_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ngay_Xuat_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Xuat_Kho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_Xuat_Kho_DM_Kho_Kho_Id",
                        column: x => x.Kho_Id,
                        principalTable: "DM_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DM_Xuat_Kho_Raw_Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Xuat_Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    San_Pham_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SL_Xuat = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Xuat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Xuat_Kho_Raw_Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_Xuat_Kho_Raw_Data_DM_San_Pham_San_Pham_Id",
                        column: x => x.San_Pham_Id,
                        principalTable: "DM_San_Pham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DM_Xuat_Kho_Raw_Data_DM_Xuat_Kho_Xuat_Kho_Id",
                        column: x => x.Xuat_Kho_Id,
                        principalTable: "DM_Xuat_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DM_Xuat_Kho_Kho_Id",
                table: "DM_Xuat_Kho",
                column: "Kho_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_Xuat_Kho_Raw_Data_San_Pham_Id",
                table: "DM_Xuat_Kho_Raw_Data",
                column: "San_Pham_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_Xuat_Kho_Raw_Data_Xuat_Kho_Id",
                table: "DM_Xuat_Kho_Raw_Data",
                column: "Xuat_Kho_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DM_Xuat_Kho_Raw_Data");

            migrationBuilder.DropTable(
                name: "DM_Xuat_Kho");
        }
    }
}
