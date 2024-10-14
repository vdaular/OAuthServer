using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth2Server.Data.Migrations.IdentityServer.PersistedGrantDb
{
    /// <inheritdoc />
    public partial class InitialIdentityServerPersistedGrantDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SEGURIDAD_INFORMATICA");

            migrationBuilder.CreateTable(
                name: "OAUTH2_DEVICE_FLOW_CODES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Data = table.Column<string>(type: "NCLOB", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_DEVICE_FLOW_CODES", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_PERSISTED_GRANTS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Key = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Data = table.Column<string>(type: "NCLOB", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_PERSISTED_GRANTS", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_DEVICE_FLOW_CODES_DeviceCode",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_DEVICE_FLOW_CODES",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_DEVICE_FLOW_CODES_Expiration",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_DEVICE_FLOW_CODES",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_PERSISTED_GRANTS_Expiration",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_PERSISTED_GRANTS",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_PERSISTED_GRANTS_SubjectId_ClientId_Type",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_PERSISTED_GRANTS",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_PERSISTED_GRANTS_SubjectId_SessionId_Type",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_PERSISTED_GRANTS",
                columns: new[] { "SubjectId", "SessionId", "Type" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OAUTH2_DEVICE_FLOW_CODES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_PERSISTED_GRANTS",
                schema: "SEGURIDAD_INFORMATICA");
        }
    }
}
