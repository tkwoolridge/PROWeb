using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_PRO_Office : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GeneralName",
                table: "PROOffices",
                newName: "RegisterName");

            migrationBuilder.AddColumn<string>(
                name: "AssistantName",
                table: "PROOffices",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssistantName",
                table: "PROOffices");

            migrationBuilder.RenameColumn(
                name: "RegisterName",
                table: "PROOffices",
                newName: "GeneralName");
        }
    }
}
