using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeInternship.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSentence7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DM_Don_Vi_Tinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten_Don_Vi_Tinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Don_Vi_Tinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_Kho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten_Kho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Kho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_Loai_San_Pham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma_LSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten_LSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Loai_San_Pham", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_NCC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma_NCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten_NCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_NCC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_Kho_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma_Dang_Nhap = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Kho_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_Kho_User_DM_Kho_Kho_Id",
                        column: x => x.Kho_Id,
                        principalTable: "DM_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DM_San_Pham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma_San_Pham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten_San_Pham = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loai_San_Pham_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Don_Vi_Tinh_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_San_Pham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_San_Pham_DM_Don_Vi_Tinh_Don_Vi_Tinh_Id",
                        column: x => x.Don_Vi_Tinh_Id,
                        principalTable: "DM_Don_Vi_Tinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DM_San_Pham_DM_Loai_San_Pham_Loai_San_Pham_Id",
                        column: x => x.Loai_San_Pham_Id,
                        principalTable: "DM_Loai_San_Pham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DM_Nhap_Kho",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NCC_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ngay_Nhap_kho = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Nhap_Kho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_Nhap_Kho_DM_Kho_Kho_Id",
                        column: x => x.Kho_Id,
                        principalTable: "DM_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DM_Nhap_Kho_DM_NCC_NCC_Id",
                        column: x => x.NCC_Id,
                        principalTable: "DM_NCC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DM_Nhap_Kho_Raw_Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nhap_Kho_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    San_Pham_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SL_Nhap = table.Column<int>(type: "int", nullable: false),
                    Don_Gia_Nhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Nhap_Kho_Raw_Data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_Nhap_Kho_Raw_Data_DM_Nhap_Kho_Nhap_Kho_Id",
                        column: x => x.Nhap_Kho_Id,
                        principalTable: "DM_Nhap_Kho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DM_Nhap_Kho_Raw_Data_DM_San_Pham_San_Pham_Id",
                        column: x => x.San_Pham_Id,
                        principalTable: "DM_San_Pham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DM_Kho_User_Kho_Id",
                table: "DM_Kho_User",
                column: "Kho_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_Kho_User_Ma_Dang_Nhap_Kho_Id",
                table: "DM_Kho_User",
                columns: new[] { "Ma_Dang_Nhap", "Kho_Id" },
                unique: true,
                filter: "[Ma_Dang_Nhap] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DM_Nhap_Kho_Kho_Id",
                table: "DM_Nhap_Kho",
                column: "Kho_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_Nhap_Kho_NCC_Id",
                table: "DM_Nhap_Kho",
                column: "NCC_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_Nhap_Kho_Raw_Data_Nhap_Kho_Id",
                table: "DM_Nhap_Kho_Raw_Data",
                column: "Nhap_Kho_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_Nhap_Kho_Raw_Data_San_Pham_Id",
                table: "DM_Nhap_Kho_Raw_Data",
                column: "San_Pham_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_San_Pham_Don_Vi_Tinh_Id",
                table: "DM_San_Pham",
                column: "Don_Vi_Tinh_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DM_San_Pham_Loai_San_Pham_Id",
                table: "DM_San_Pham",
                column: "Loai_San_Pham_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DM_Kho_User");

            migrationBuilder.DropTable(
                name: "DM_Nhap_Kho_Raw_Data");

            migrationBuilder.DropTable(
                name: "DM_Nhap_Kho");

            migrationBuilder.DropTable(
                name: "DM_San_Pham");

            migrationBuilder.DropTable(
                name: "DM_Kho");

            migrationBuilder.DropTable(
                name: "DM_NCC");

            migrationBuilder.DropTable(
                name: "DM_Don_Vi_Tinh");

            migrationBuilder.DropTable(
                name: "DM_Loai_San_Pham");
        }
    }
}
