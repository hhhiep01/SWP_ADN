using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixTestOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSampleMethods_Services_ServiceId1",
                table: "ServiceSampleMethods");

            migrationBuilder.DropIndex(
                name: "IX_ServiceSampleMethods_ServiceId1",
                table: "ServiceSampleMethods");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "ServiceSampleMethods");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TestOrders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "TestOrders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "TestOrders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "TestOrders");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "TestOrders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "TestOrders");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId1",
                table: "ServiceSampleMethods",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSampleMethods_ServiceId1",
                table: "ServiceSampleMethods",
                column: "ServiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSampleMethods_Services_ServiceId1",
                table: "ServiceSampleMethods",
                column: "ServiceId1",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
