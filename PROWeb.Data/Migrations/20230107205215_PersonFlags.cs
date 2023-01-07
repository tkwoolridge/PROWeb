using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterVoterFlag_VoterFlags_VoterFlagId",
                table: "VoterVoterFlag");

            migrationBuilder.RenameColumn(
                name: "VoterFlagId",
                table: "VoterVoterFlag",
                newName: "PersonFlagId");

            migrationBuilder.RenameColumn(
                name: "VoterFlagId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoterVoterFlag_VoterFlags_PersonFlagId",
                table: "VoterVoterFlag");

            migrationBuilder.RenameColumn(
                name: "PersonFlagId",
                table: "VoterVoterFlag",
                newName: "VoterFlagId");

            migrationBuilder.RenameColumn(
                name: "PersonFlagId",
                table: "VoterFlags",
                newName: "VoterFlagId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoterVoterFlag_VoterFlags_VoterFlagId",
                table: "VoterVoterFlag",
                column: "VoterFlagId",
                principalTable: "VoterFlags",
                principalColumn: "VoterFlagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
