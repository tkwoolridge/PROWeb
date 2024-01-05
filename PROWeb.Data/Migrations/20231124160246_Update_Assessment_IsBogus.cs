using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Assessment_IsBogus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Constituencies_BogusNo",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Constituencies_OldBogusNo",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Voters_Constituencies_BogusNo",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Voters_BogusNo",
                table: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_BogusNo",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_OldBogusNo",
                table: "Registrations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Constituencies_BogusNo",
                table: "Constituencies");

            migrationBuilder.DropColumn(
                name: "BogusNo",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "IsBogusNo",
                table: "Voters");

            migrationBuilder.DropColumn(
                name: "BogusNo",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "IsBogusNo",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "OldBogusNo",
                table: "Registrations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BogusNo",
                table: "Voters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBogusNo",
                table: "Voters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BogusNo",
                table: "Registrations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBogusNo",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OldBogusNo",
                table: "Registrations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Constituencies_BogusNo",
                table: "Constituencies",
                column: "BogusNo");

            migrationBuilder.CreateIndex(
                name: "IX_Voters_BogusNo",
                table: "Voters",
                column: "BogusNo");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_BogusNo",
                table: "Registrations",
                column: "BogusNo");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_OldBogusNo",
                table: "Registrations",
                column: "OldBogusNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Constituencies_BogusNo",
                table: "Registrations",
                column: "BogusNo",
                principalTable: "Constituencies",
                principalColumn: "BogusNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Constituencies_OldBogusNo",
                table: "Registrations",
                column: "OldBogusNo",
                principalTable: "Constituencies",
                principalColumn: "BogusNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Voters_Constituencies_BogusNo",
                table: "Voters",
                column: "BogusNo",
                principalTable: "Constituencies",
                principalColumn: "BogusNo");
        }
    }
}
