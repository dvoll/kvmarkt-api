using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KvMarktApi.Migrations
{
    public partial class PlacesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Association",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LogoUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Association", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "scheme_categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheme_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "scheme_places",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheme_places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contributor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Association = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Firstname = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributor_Association_Association",
                        column: x => x.Association,
                        principalTable: "Association",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Association = table.Column<long>(nullable: false),
                    LogoUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Association_Association",
                        column: x => x.Association,
                        principalTable: "Association",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    age_end = table.Column<int>(nullable: false),
                    age_start = table.Column<int>(nullable: false),
                    Author = table.Column<long>(nullable: true),
                    Category = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schemes_Contributor_Author",
                        column: x => x.Author,
                        principalTable: "Contributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schemes_scheme_categories_Category",
                        column: x => x.Category,
                        principalTable: "scheme_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_contributorOfTeam",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Contributor = table.Column<long>(nullable: false),
                    Team = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_contributorOfTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_team_contributorOfTeam_Contributor_Contributor",
                        column: x => x.Contributor,
                        principalTable: "Contributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_contributorOfTeam_Team_Team",
                        column: x => x.Team,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contributor_favoriteSchemes",
                columns: table => new
                {
                    Scheme = table.Column<long>(nullable: false),
                    Contributor = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contributor_favoriteSchemes", x => new { x.Scheme, x.Contributor });
                    table.UniqueConstraint("AK_contributor_favoriteSchemes_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contributor_favoriteSchemes_Contributor_Contributor",
                        column: x => x.Contributor,
                        principalTable: "Contributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contributor_favoriteSchemes_Schemes_Scheme",
                        column: x => x.Scheme,
                        principalTable: "Schemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scheme_selectedPlaces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Place = table.Column<long>(nullable: false),
                    Scheme = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scheme_selectedPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_scheme_selectedPlaces_scheme_places_Place",
                        column: x => x.Place,
                        principalTable: "scheme_places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scheme_selectedPlaces_Schemes_Scheme",
                        column: x => x.Scheme,
                        principalTable: "Schemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contributor_Association",
                table: "Contributor",
                column: "Association");

            migrationBuilder.CreateIndex(
                name: "IX_contributor_favoriteSchemes_Contributor",
                table: "contributor_favoriteSchemes",
                column: "Contributor");

            migrationBuilder.CreateIndex(
                name: "IX_scheme_selectedPlaces_Place",
                table: "scheme_selectedPlaces",
                column: "Place");

            migrationBuilder.CreateIndex(
                name: "IX_scheme_selectedPlaces_Scheme",
                table: "scheme_selectedPlaces",
                column: "Scheme");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Author",
                table: "Schemes",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Category",
                table: "Schemes",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Association",
                table: "Team",
                column: "Association");

            migrationBuilder.CreateIndex(
                name: "IX_team_contributorOfTeam_Contributor",
                table: "team_contributorOfTeam",
                column: "Contributor");

            migrationBuilder.CreateIndex(
                name: "IX_team_contributorOfTeam_Team",
                table: "team_contributorOfTeam",
                column: "Team");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "contributor_favoriteSchemes");

            migrationBuilder.DropTable(
                name: "scheme_selectedPlaces");

            migrationBuilder.DropTable(
                name: "team_contributorOfTeam");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "scheme_places");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Contributor");

            migrationBuilder.DropTable(
                name: "scheme_categories");

            migrationBuilder.DropTable(
                name: "Association");
        }
    }
}
