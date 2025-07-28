using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixResult2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Samples_SampleId",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "SampleId",
                table: "Results",
                newName: "TestOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_SampleId",
                table: "Results",
                newName: "IX_Results_TestOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_TestOrders_TestOrderId",
                table: "Results",
                column: "TestOrderId",
                principalTable: "TestOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_TestOrders_TestOrderId",
                table: "Results");

            migrationBuilder.RenameColumn(
                name: "TestOrderId",
                table: "Results",
                newName: "SampleId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_TestOrderId",
                table: "Results",
                newName: "IX_Results_SampleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Samples_SampleId",
                table: "Results",
                column: "SampleId",
                principalTable: "Samples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
