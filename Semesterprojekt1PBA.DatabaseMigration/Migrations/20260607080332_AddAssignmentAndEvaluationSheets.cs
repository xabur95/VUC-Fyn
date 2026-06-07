using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Semesterprojekt1PBA.DatabaseMigration.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentAndEvaluationSheets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentSheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentSheets_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSheets_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationSheets_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentSheetQuestions",
                columns: table => new
                {
                    AssignmentSheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSheetQuestions", x => new { x.AssignmentSheetId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_AssignmentSheetQuestions_AssignmentSheets_AssignmentSheetId",
                        column: x => x.AssignmentSheetId,
                        principalTable: "AssignmentSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentSheetQuestions_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentSheetTopics",
                columns: table => new
                {
                    AssignmentSheetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSheetTopics", x => new { x.AssignmentSheetId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_AssignmentSheetTopics_AssignmentSheets_AssignmentSheetId",
                        column: x => x.AssignmentSheetId,
                        principalTable: "AssignmentSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentSheetTopics_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    ScoredByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluationSheetStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EvaluationSheetTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionScores_EvaluationSheets_EvaluationSheetStudentId",
                        column: x => x.EvaluationSheetStudentId,
                        principalTable: "EvaluationSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionScores_EvaluationSheets_EvaluationSheetTeacherId",
                        column: x => x.EvaluationSheetTeacherId,
                        principalTable: "EvaluationSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSheetQuestions_QuestionsId",
                table: "AssignmentSheetQuestions",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSheets_AuthorId",
                table: "AssignmentSheets",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSheets_SubjectId",
                table: "AssignmentSheets",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSheetTopics_TopicsId",
                table: "AssignmentSheetTopics",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSheets_ClassId",
                table: "EvaluationSheets",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionScores_EvaluationSheetStudentId",
                table: "QuestionScores",
                column: "EvaluationSheetStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionScores_EvaluationSheetTeacherId",
                table: "QuestionScores",
                column: "EvaluationSheetTeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentSheetQuestions");

            migrationBuilder.DropTable(
                name: "AssignmentSheetTopics");

            migrationBuilder.DropTable(
                name: "QuestionScores");

            migrationBuilder.DropTable(
                name: "AssignmentSheets");

            migrationBuilder.DropTable(
                name: "EvaluationSheets");
        }
    }
}
