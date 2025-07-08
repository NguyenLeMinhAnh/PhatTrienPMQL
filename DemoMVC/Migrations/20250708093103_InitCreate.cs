using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaiLys_HeThongPhanPhois_HeThongPhanPhoiMaHTPP",
                table: "DaiLys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeThongPhanPhois",
                table: "HeThongPhanPhois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaiLys",
                table: "DaiLys");

            migrationBuilder.RenameTable(
                name: "HeThongPhanPhois",
                newName: "HeThongPhanPhoi");

            migrationBuilder.RenameTable(
                name: "DaiLys",
                newName: "DaiLy");

            migrationBuilder.RenameIndex(
                name: "IX_DaiLys_HeThongPhanPhoiMaHTPP",
                table: "DaiLy",
                newName: "IX_DaiLy_HeThongPhanPhoiMaHTPP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeThongPhanPhoi",
                table: "HeThongPhanPhoi",
                column: "MaHTPP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaiLy",
                table: "DaiLy",
                column: "MaDaiLy");

            migrationBuilder.AddForeignKey(
                name: "FK_DaiLy_HeThongPhanPhoi_HeThongPhanPhoiMaHTPP",
                table: "DaiLy",
                column: "HeThongPhanPhoiMaHTPP",
                principalTable: "HeThongPhanPhoi",
                principalColumn: "MaHTPP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaiLy_HeThongPhanPhoi_HeThongPhanPhoiMaHTPP",
                table: "DaiLy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeThongPhanPhoi",
                table: "HeThongPhanPhoi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaiLy",
                table: "DaiLy");

            migrationBuilder.RenameTable(
                name: "HeThongPhanPhoi",
                newName: "HeThongPhanPhois");

            migrationBuilder.RenameTable(
                name: "DaiLy",
                newName: "DaiLys");

            migrationBuilder.RenameIndex(
                name: "IX_DaiLy_HeThongPhanPhoiMaHTPP",
                table: "DaiLys",
                newName: "IX_DaiLys_HeThongPhanPhoiMaHTPP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeThongPhanPhois",
                table: "HeThongPhanPhois",
                column: "MaHTPP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaiLys",
                table: "DaiLys",
                column: "MaDaiLy");

            migrationBuilder.AddForeignKey(
                name: "FK_DaiLys_HeThongPhanPhois_HeThongPhanPhoiMaHTPP",
                table: "DaiLys",
                column: "HeThongPhanPhoiMaHTPP",
                principalTable: "HeThongPhanPhois",
                principalColumn: "MaHTPP");
        }
    }
}
