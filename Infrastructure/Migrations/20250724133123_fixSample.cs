using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixSample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParticipantName",
                table: "Samples",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                table: "Samples",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SampleCode",
                table: "Samples",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocusResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SampleId = table.Column<int>(type: "integer", nullable: false),
                    LocusName = table.Column<string>(type: "text", nullable: false),
                    FirstAllele = table.Column<string>(type: "text", nullable: false),
                    SecondAllele = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocusResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocusResults_Samples_SampleId",
                        column: x => x.SampleId,
                        principalTable: "Samples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocusResults_SampleId",
                table: "LocusResults",
                column: "SampleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocusResults");

            migrationBuilder.DropColumn(
                name: "ParticipantName",
                table: "Samples");

            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "Samples");

            migrationBuilder.DropColumn(
                name: "SampleCode",
                table: "Samples");
        }
    }
}
