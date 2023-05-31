using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegistrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Voters_VoterId_RegistryYear",
                table: "Registrations");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_VoterId_RegistryYear",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "IsEligible",
                table: "Registrations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEligible",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_VoterId_RegistryYear",
                table: "Registrations",
                columns: new[] { "VoterId", "RegistryYear" });

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Voters_VoterId_RegistryYear",
                table: "Registrations",
                columns: new[] { "VoterId", "RegistryYear" },
                principalTable: "Voters",
                principalColumns: new[] { "VoterId", "RegistryYear" });
        }
    }
}
