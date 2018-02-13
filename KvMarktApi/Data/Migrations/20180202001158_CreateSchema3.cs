using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KvMarktApi.Data.Migrations
{
    public partial class CreateSchema3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contributor_favoriteSchemes_Schemes_Scheme",
                table: "contributor_favoriteSchemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes_Contributor_Author",
                table: "Schemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes_scheme_categories_Category",
                table: "Schemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes_scheme_places_Place2",
                table: "Schemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes_scheme_places_Place3",
                table: "Schemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes_scheme_places_Place",
                table: "Schemes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schemes",
                table: "Schemes");

            migrationBuilder.RenameTable(
                name: "Schemes",
                newName: "Schemes2");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes_Place",
                table: "Schemes2",
                newName: "IX_Schemes2_Place");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes_Place3",
                table: "Schemes2",
                newName: "IX_Schemes2_Place3");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes_Place2",
                table: "Schemes2",
                newName: "IX_Schemes2_Place2");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes_Category",
                table: "Schemes2",
                newName: "IX_Schemes2_Category");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes_Author",
                table: "Schemes2",
                newName: "IX_Schemes2_Author");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schemes2",
                table: "Schemes2",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contributor_favoriteSchemes_Schemes2_Scheme",
                table: "contributor_favoriteSchemes",
                column: "Scheme",
                principalTable: "Schemes2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes2_Contributor_Author",
                table: "Schemes2",
                column: "Author",
                principalTable: "Contributor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes2_scheme_categories_Category",
                table: "Schemes2",
                column: "Category",
                principalTable: "scheme_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes2_scheme_places_Place2",
                table: "Schemes2",
                column: "Place2",
                principalTable: "scheme_places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes2_scheme_places_Place3",
                table: "Schemes2",
                column: "Place3",
                principalTable: "scheme_places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes2_scheme_places_Place",
                table: "Schemes2",
                column: "Place",
                principalTable: "scheme_places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contributor_favoriteSchemes_Schemes2_Scheme",
                table: "contributor_favoriteSchemes");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes2_Contributor_Author",
                table: "Schemes2");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes2_scheme_categories_Category",
                table: "Schemes2");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes2_scheme_places_Place2",
                table: "Schemes2");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes2_scheme_places_Place3",
                table: "Schemes2");

            migrationBuilder.DropForeignKey(
                name: "FK_Schemes2_scheme_places_Place",
                table: "Schemes2");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schemes2",
                table: "Schemes2");

            migrationBuilder.RenameTable(
                name: "Schemes2",
                newName: "Schemes");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes2_Place",
                table: "Schemes",
                newName: "IX_Schemes_Place");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes2_Place3",
                table: "Schemes",
                newName: "IX_Schemes_Place3");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes2_Place2",
                table: "Schemes",
                newName: "IX_Schemes_Place2");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes2_Category",
                table: "Schemes",
                newName: "IX_Schemes_Category");

            migrationBuilder.RenameIndex(
                name: "IX_Schemes2_Author",
                table: "Schemes",
                newName: "IX_Schemes_Author");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schemes",
                table: "Schemes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contributor_favoriteSchemes_Schemes_Scheme",
                table: "contributor_favoriteSchemes",
                column: "Scheme",
                principalTable: "Schemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes_Contributor_Author",
                table: "Schemes",
                column: "Author",
                principalTable: "Contributor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes_scheme_categories_Category",
                table: "Schemes",
                column: "Category",
                principalTable: "scheme_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes_scheme_places_Place2",
                table: "Schemes",
                column: "Place2",
                principalTable: "scheme_places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes_scheme_places_Place3",
                table: "Schemes",
                column: "Place3",
                principalTable: "scheme_places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schemes_scheme_places_Place",
                table: "Schemes",
                column: "Place",
                principalTable: "scheme_places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
