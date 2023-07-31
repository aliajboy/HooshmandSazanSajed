using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hooshmand.Migrations
{
    /// <inheritdoc />
    public partial class addcustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_PhoneBooks_PhoneBooksId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PhoneBookId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "PhoneBooksId",
                table: "Projects",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_PhoneBooksId",
                table: "Projects",
                newName: "IX_Projects_CustomerId");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneBooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_PhoneBooks_PhoneBooksId",
                        column: x => x.PhoneBooksId,
                        principalTable: "PhoneBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneBooksId",
                table: "Customers",
                column: "PhoneBooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Projects",
                newName: "PhoneBooksId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                newName: "IX_Projects_PhoneBooksId");

            migrationBuilder.AddColumn<int>(
                name: "PhoneBookId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_PhoneBooks_PhoneBooksId",
                table: "Projects",
                column: "PhoneBooksId",
                principalTable: "PhoneBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
