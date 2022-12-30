using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class VoterDocumentToDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoterDocuments");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    RegistryYear = table.Column<int>(type: "int", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DocumentDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExportFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Voters_PersonId_RegistryYear",
                        columns: x => new { x.PersonId, x.RegistryYear },
                        principalTable: "Voters",
                        principalColumns: new[] { "VoterId", "RegistryYear" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_PersonId_RegistryYear",
                table: "Documents",
                columns: new[] { "PersonId", "RegistryYear" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.CreateTable(
                name: "VoterDocuments",
                columns: table => new
                {
                    VoterDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    RegistryYear = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExportFormat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoterDocuments", x => x.VoterDocumentId);
                    table.ForeignKey(
                        name: "FK_VoterDocuments_Voters_VoterId_RegistryYear",
                        columns: x => new { x.VoterId, x.RegistryYear },
                        principalTable: "Voters",
                        principalColumns: new[] { "VoterId", "RegistryYear" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoterDocuments_VoterId_RegistryYear",
                table: "VoterDocuments",
                columns: new[] { "VoterId", "RegistryYear" });
        }
    }
}
