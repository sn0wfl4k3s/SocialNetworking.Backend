using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraData.Migrations
{
    public partial class PostCommentFilesIncluded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "Net");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Net",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Birth = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Net",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "Net",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "Net",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<ulong>(nullable: true),
                    AuthorId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "Net",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Net",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "Net",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: false),
                    Size = table.Column<long>(nullable: false),
                    Sended = table.Column<DateTime>(nullable: false),
                    FileType = table.Column<int>(nullable: false),
                    OwnerId = table.Column<ulong>(nullable: true),
                    CommentId = table.Column<ulong>(nullable: true),
                    PostId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "Net",
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "Net",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Net",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                schema: "Net",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                schema: "Net",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CommentId",
                schema: "Net",
                table: "Files",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_OwnerId",
                schema: "Net",
                table: "Files",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_PostId",
                schema: "Net",
                table: "Files",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                schema: "Net",
                table: "Posts",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files",
                schema: "Net");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "Net");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Net");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Net");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Net",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }
    }
}
