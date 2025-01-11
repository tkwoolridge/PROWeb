using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Center_Cordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConstituencyBoundaries",
                columns: table => new
                {
                    ConstituencyId = table.Column<int>(type: "int", nullable: false),
                    ConstituencyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geometry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterLongitude = table.Column<double>(type: "float", nullable: false),
                    CenterLatitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstituencyBoundaries", x => x.ConstituencyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstituencyBoundaries");
        }
    }
}
