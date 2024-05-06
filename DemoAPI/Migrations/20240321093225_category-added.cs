using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAPI.Migrations
{
    /// <inheritdoc />
    public partial class categoryadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PId",
                table: "Products",
                newName: "PID");

            migrationBuilder.AddColumn<int>(
                name: "CateId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryDID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    DID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.DID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryDID",
                table: "Products",
                column: "CategoryDID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryDID",
                table: "Products",
                column: "CategoryDID",
                principalTable: "Categories",
                principalColumn: "DID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryDID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryDID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CateId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryDID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PID",
                table: "Products",
                newName: "PId");
        }
    }
}
