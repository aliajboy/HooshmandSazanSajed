using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hooshmand.Migrations
{
    /// <inheritdoc />
    public partial class removeprojectmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomer",
                table: "PhoneBooks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                table: "PhoneBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
