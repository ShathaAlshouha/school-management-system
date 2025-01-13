using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Migrations
{
    /// <inheritdoc />
    public partial class updatepaymentclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_ClassId",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClassId",
                table: "Payments",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_ClassId",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClassId",
                table: "Payments",
                column: "ClassId",
                unique: true);
        }
    }
}
