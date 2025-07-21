using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_AspNetUsers_FullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaiLy_HeThongPhanPhoi_HeThongPhanPhoiMaHTPP",
                table: "DaiLy");

            migrationBuilder.DropIndex(
                name: "IX_DaiLy_HeThongPhanPhoiMaHTPP",
                table: "DaiLy");

            migrationBuilder.DropColumn(
                name: "HeThongPhanPhoiMaHTPP",
                table: "DaiLy");

            migrationBuilder.CreateIndex(
                name: "IX_DaiLy_MaHTPP",
                table: "DaiLy",
                column: "MaHTPP");

            migrationBuilder.AddForeignKey(
                name: "FK_DaiLy_HeThongPhanPhoi_MaHTPP",
                table: "DaiLy",
                column: "MaHTPP",
                principalTable: "HeThongPhanPhoi",
                principalColumn: "MaHTPP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaiLy_HeThongPhanPhoi_MaHTPP",
                table: "DaiLy");

            migrationBuilder.DropIndex(
                name: "IX_DaiLy_MaHTPP",
                table: "DaiLy");

            migrationBuilder.AddColumn<string>(
                name: "HeThongPhanPhoiMaHTPP",
                table: "DaiLy",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DaiLy_HeThongPhanPhoiMaHTPP",
                table: "DaiLy",
                column: "HeThongPhanPhoiMaHTPP");

            migrationBuilder.AddForeignKey(
                name: "FK_DaiLy_HeThongPhanPhoi_HeThongPhanPhoiMaHTPP",
                table: "DaiLy",
                column: "HeThongPhanPhoiMaHTPP",
                principalTable: "HeThongPhanPhoi",
                principalColumn: "MaHTPP");
        }
    }
}
