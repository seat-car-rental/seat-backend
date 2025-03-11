using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatAPI.Migrations
{
    /// <inheritdoc />
    public partial class deleteOtpemailorphonefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOrPhone",
                table: "Otps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailOrPhone",
                table: "Otps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
