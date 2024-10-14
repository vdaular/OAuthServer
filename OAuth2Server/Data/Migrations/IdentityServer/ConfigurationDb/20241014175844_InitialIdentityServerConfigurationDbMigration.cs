using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth2Server.Data.Migrations.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class InitialIdentityServerConfigurationDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SEGURIDAD_INFORMATICA");

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_RESOURCES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    AllowedAccessTokenSigningAlgorithms = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    NonEditable = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_RESOURCES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_SCOPES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Emphasize = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_SCOPES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENTS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ClientId = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ClientName = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    AllowRememberConsent = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    RequirePkce = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    RequireRequestObject = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    BackChannelLogoutUri = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    AllowOfflineAccess = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IdentityTokenLifetime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AllowedIdentityTokenSigningAlgorithms = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    AccessTokenLifetime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ConsentLifetime = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RefreshTokenUsage = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    RefreshTokenExpiration = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AccessTokenType = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EnableLocalLogin = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    IncludeJwtId = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ClientClaimsPrefix = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LastAccessed = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    UserSsoLifetime = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    UserCodeType = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NonEditable = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENTS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_IDENTITY_RESOURCES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Emphasize = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    NonEditable = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_IDENTITY_RESOURCES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_RESOURCE_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ApiResourceId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_RESOURCE_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_API_RESOURCE_CLAIMS_OAUTH2_API_RESOURCES_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_API_RESOURCES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_RESOURCE_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ApiResourceId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Key = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_RESOURCE_PROPERTIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_API_RESOURCE_PROPERTIES_OAUTH2_API_RESOURCES_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_API_RESOURCES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_RESOURCE_SCOPES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Scope = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_RESOURCE_SCOPES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_API_RESOURCE_SCOPES_OAUTH2_API_RESOURCES_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_API_RESOURCES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_RESOURCE_SECRETS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ApiResourceId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    Value = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Type = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_RESOURCE_SECRETS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_API_RESOURCE_SECRETS_OAUTH2_API_RESOURCES_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_API_RESOURCES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_SCOPE_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ScopeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_SCOPE_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_API_SCOPE_CLAIMS_OAUTH2_API_SCOPES_ScopeId",
                        column: x => x.ScopeId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_API_SCOPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_API_SCOPE_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ScopeId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Key = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_API_SCOPE_PROPERTIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_API_SCOPE_PROPERTIES_OAUTH2_API_SCOPES_ScopeId",
                        column: x => x.ScopeId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_API_SCOPES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Type = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_CLAIMS_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_CORS_ORIGINS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Origin = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_CORS_ORIGINS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_CORS_ORIGINS_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_GRANT_TYPES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    GrantType = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_GRANT_TYPES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_GRANT_TYPES_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_IDP_RESTRICTIONS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Provider = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_IDP_RESTRICTIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_IDP_RESTRICTIONS_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_POST_LOGOUT_REDIRECT_URIS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PostLogoutRedirectUri = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_POST_LOGOUT_REDIRECT_URIS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_POST_LOGOUT_REDIRECT_URIS_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Key = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_PROPERTIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_PROPERTIES_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_REDIRECT_URIS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RedirectUri = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_REDIRECT_URIS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_REDIRECT_URIS_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_SCOPES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Scope = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_SCOPES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_SCOPES_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_CLIENT_SECRETS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    Value = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Type = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_CLIENT_SECRETS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_CLIENT_SECRETS_OAUTH2_CLIENTS_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_CLIENTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_IDENTITY_RESOURCE_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdentityResourceId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Type = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_IDENTITY_RESOURCE_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_IDENTITY_RESOURCE_CLAIMS_OAUTH2_IDENTITY_RESOURCES_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_IDENTITY_RESOURCES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAUTH2_IDENTITY_RESOURCE_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdentityResourceId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Key = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAUTH2_IDENTITY_RESOURCE_PROPERTIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAUTH2_IDENTITY_RESOURCE_PROPERTIES_OAUTH2_IDENTITY_RESOURCES_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalSchema: "SEGURIDAD_INFORMATICA",
                        principalTable: "OAUTH2_IDENTITY_RESOURCES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_RESOURCE_CLAIMS_ApiResourceId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_RESOURCE_CLAIMS",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_RESOURCE_PROPERTIES_ApiResourceId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_RESOURCE_PROPERTIES",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_RESOURCE_SCOPES_ApiResourceId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_RESOURCE_SCOPES",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_RESOURCE_SECRETS_ApiResourceId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_RESOURCE_SECRETS",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_RESOURCES_Name",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_RESOURCES",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_SCOPE_CLAIMS_ScopeId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_SCOPE_CLAIMS",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_SCOPE_PROPERTIES_ScopeId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_SCOPE_PROPERTIES",
                column: "ScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_API_SCOPES_Name",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_API_SCOPES",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_CLAIMS_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_CLAIMS",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_CORS_ORIGINS_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_CORS_ORIGINS",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_GRANT_TYPES_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_GRANT_TYPES",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_IDP_RESTRICTIONS_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_IDP_RESTRICTIONS",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_POST_LOGOUT_REDIRECT_URIS_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_POST_LOGOUT_REDIRECT_URIS",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_PROPERTIES_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_PROPERTIES",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_REDIRECT_URIS_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_REDIRECT_URIS",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_SCOPES_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_SCOPES",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENT_SECRETS_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENT_SECRETS",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_CLIENTS_ClientId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_CLIENTS",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_IDENTITY_RESOURCE_CLAIMS_IdentityResourceId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_IDENTITY_RESOURCE_CLAIMS",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_IDENTITY_RESOURCE_PROPERTIES_IdentityResourceId",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_IDENTITY_RESOURCE_PROPERTIES",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OAUTH2_IDENTITY_RESOURCES_Name",
                schema: "SEGURIDAD_INFORMATICA",
                table: "OAUTH2_IDENTITY_RESOURCES",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OAUTH2_API_RESOURCE_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_API_RESOURCE_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_API_RESOURCE_SCOPES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_API_RESOURCE_SECRETS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_API_SCOPE_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_API_SCOPE_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_CORS_ORIGINS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_GRANT_TYPES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_IDP_RESTRICTIONS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_POST_LOGOUT_REDIRECT_URIS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_REDIRECT_URIS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_SCOPES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENT_SECRETS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_IDENTITY_RESOURCE_CLAIMS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_IDENTITY_RESOURCE_PROPERTIES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_API_RESOURCES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_API_SCOPES",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_CLIENTS",
                schema: "SEGURIDAD_INFORMATICA");

            migrationBuilder.DropTable(
                name: "OAUTH2_IDENTITY_RESOURCES",
                schema: "SEGURIDAD_INFORMATICA");
        }
    }
}
