using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SozlukApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "emailconfirmation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldEmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewEmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailconfirmation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entry",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryComments_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryComments_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryfavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryfavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryfavorite_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entryfavorite_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryvote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryvote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryvote_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryCommentFavorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryCommentFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryCommentFavorites_EntryComments_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalTable: "EntryComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryCommentFavorites_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryCommentVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryCommentVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryCommentVotes_EntryComments_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalTable: "EntryComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entry_CreatedById",
                schema: "dbo",
                table: "entry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorites_CreatedById",
                table: "EntryCommentFavorites",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentFavorites_EntryCommentId",
                table: "EntryCommentFavorites",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComments_CreatedById",
                table: "EntryComments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntryComments_EntryId",
                table: "EntryComments",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryCommentVotes_EntryCommentId",
                table: "EntryCommentVotes",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entryfavorite_CreatedById",
                schema: "dbo",
                table: "entryfavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entryfavorite_EntryId",
                schema: "dbo",
                table: "entryfavorite",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entryvote_EntryId",
                schema: "dbo",
                table: "entryvote",
                column: "EntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emailconfirmation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryCommentFavorites");

            migrationBuilder.DropTable(
                name: "EntryCommentVotes");

            migrationBuilder.DropTable(
                name: "entryfavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryvote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntryComments");

            migrationBuilder.DropTable(
                name: "entry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user",
                schema: "dbo");
        }
    }
}
