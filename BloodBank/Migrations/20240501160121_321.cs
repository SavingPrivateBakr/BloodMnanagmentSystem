using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBank.Migrations
{
    /// <inheritdoc />
    public partial class _321 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NurseId",
                table: "Bloods");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Hospitals",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Hospitals",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "NurseId",
                table: "Bloods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
