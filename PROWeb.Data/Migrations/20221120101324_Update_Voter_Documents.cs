using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    public partial class Update_Voter_Documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistryYear",
                table: "VoterDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VoterDocuments_VoterId_RegistryYear",
                table: "VoterDocuments",
                columns: new[] { "VoterId", "RegistryYear" });

            migrationBuilder.AddForeignKey(
                name: "FK_VoterDocuments_Voters_VoterId_RegistryYear",
                table: "VoterDocuments",
                columns: new[] { "VoterId", "RegistryYear" },
                principalTable: "Voters",
                principalColumns: new[] { "VoterId", "RegistryYear" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterDocuments_Voters_VoterId_RegistryYear",
                table: "VoterDocuments");

            migrationBuilder.DropIndex(
                name: "IX_VoterDocuments_VoterId_RegistryYear",
                table: "VoterDocuments");

            migrationBuilder.DropColumn(
                name: "RegistryYear",
                table: "VoterDocuments");
        }
    }
}
