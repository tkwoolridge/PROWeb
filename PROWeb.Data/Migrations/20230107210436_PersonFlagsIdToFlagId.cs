using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonFlagsIdToFlagId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterVoterFlag_VoterFlags_PersonFlagId",
                table: "VoterVoterFlag");

            migrationBuilder.RenameColumn(
                name: "PersonFlagId",
                table: "VoterVoterFlag",
                newName: "FlagId");

            migrationBuilder.RenameColumn(
                name: "PersonFlagId",
                table: "VoterFlags",
                newName: "FlagId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterVoterFlag_VoterFlags_FlagId",
                table: "VoterVoterFlag",
                column: "FlagId",
                principalTable: "VoterFlags",
                principalColumn: "FlagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterVoterFlag_VoterFlags_FlagId",
                table: "VoterVoterFlag");

            migrationBuilder.RenameColumn(
                name: "FlagId",
                table: "VoterVoterFlag",
                newName: "PersonFlagId");

            migrationBuilder.RenameColumn(
                name: "FlagId",
                table: "VoterFlags",
                newName: "PersonFlagId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterVoterFlag_VoterFlags_PersonFlagId",
                table: "VoterVoterFlag",
                column: "PersonFlagId",
                principalTable: "VoterFlags",
                principalColumn: "PersonFlagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
