using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KvMarktApi.Data.Migrations
{
    public partial class CreateSchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUserClaims",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoleClaims",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

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
                    Place2 = table.Column<long>(nullable: true),
                    Place3 = table.Column<long>(nullable: true),
                    Place = table.Column<long>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Schemes_scheme_places_Place2",
                        column: x => x.Place2,
                        principalTable: "scheme_places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schemes_scheme_places_Place3",
                        column: x => x.Place3,
                        principalTable: "scheme_places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schemes_scheme_places_Place",
                        column: x => x.Place,
                        principalTable: "scheme_places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contributor_favoriteSchemes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Contributor = table.Column<long>(nullable: false),
                    Scheme = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contributor_favoriteSchemes", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contributor_favoriteSchemes_Contributor",
                table: "contributor_favoriteSchemes",
                column: "Contributor");

            migrationBuilder.CreateIndex(
                name: "IX_contributor_favoriteSchemes_Scheme",
                table: "contributor_favoriteSchemes",
                column: "Scheme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Author",
                table: "Schemes",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Category",
                table: "Schemes",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Place2",
                table: "Schemes",
                column: "Place2");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Place3",
                table: "Schemes",
                column: "Place3");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Place",
                table: "Schemes",
                column: "Place");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "contributor_favoriteSchemes");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropTable(
                name: "Contributor");

            migrationBuilder.DropTable(
                name: "scheme_categories");

            migrationBuilder.DropTable(
                name: "scheme_places");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUserClaims",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoleClaims",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
