using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTestOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId1",
                table: "ServiceSampleMethods",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SampleMethods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TestOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DeliveryKitStatus = table.Column<int>(type: "integer", nullable: false),
                    KitSendDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SampleMethodId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AppointmentStatus = table.Column<int>(type: "integer", nullable: false),
                    AppointmentLocation = table.Column<string>(type: "text", nullable: false),
                    AppointmentStaffId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestOrders_SampleMethods_SampleMethodId",
                        column: x => x.SampleMethodId,
                        principalTable: "SampleMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestOrders_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestOrders_Users_AppointmentStaffId",
                        column: x => x.AppointmentStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSampleMethods_ServiceId1",
                table: "ServiceSampleMethods",
                column: "ServiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_TestOrders_AppointmentStaffId",
                table: "TestOrders",
                column: "AppointmentStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOrders_SampleMethodId",
                table: "TestOrders",
                column: "SampleMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOrders_ServiceId",
                table: "TestOrders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOrders_UserId",
                table: "TestOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceSampleMethods_Services_ServiceId1",
                table: "ServiceSampleMethods",
                column: "ServiceId1",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceSampleMethods_Services_ServiceId1",
                table: "ServiceSampleMethods");

            migrationBuilder.DropTable(
                name: "TestOrders");

            migrationBuilder.DropIndex(
                name: "IX_ServiceSampleMethods_ServiceId1",
                table: "ServiceSampleMethods");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "ServiceSampleMethods");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SampleMethods");
        }
    }
}
