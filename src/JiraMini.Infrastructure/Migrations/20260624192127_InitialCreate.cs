using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JiraMini.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_projects_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "project_members",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    joined_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_members_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_members_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assignee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_tasks_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tasks_users_assignee_id",
                        column: x => x.assignee_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    file_path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    uploaded_by = table.Column<Guid>(type: "uuid", nullable: false),
                    uploaded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => x.id);
                    table.ForeignKey(
                        name: "FK_attachments_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_attachments_users_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "full_name", "password_hash", "role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@jiramini.com", "Admin System", "hashed_password_1", 2 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "a@jiramini.com", "Nguyen Van A", "hashed_password_2", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "b@jiramini.com", "Tran Van B", "hashed_password_3", 1 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "c@jiramini.com", "Le Van C", "hashed_password_4", 1 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "d@jiramini.com", "Pham Van D", "hashed_password_5", 1 }
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "id", "created_at", "description", "name", "owner_id" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Core system development", "JiraMini Core", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Login and JWT", "Authentication", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Manage tasks", "Task Management", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Email notifications", "Notification", new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Generate reports", "Reporting", new Guid("55555555-5555-5555-5555-555555555555") }
                });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "id", "assignee_id", "created_at", "description", "due_date", "priority", "project_id", "status", "title" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Create project structure", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 1, "Setup Clean Architecture" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Add authentication", new DateTime(2026, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 2, "Implement JWT" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "CRUD task endpoints", new DateTime(2026, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 3, "Create Task API" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Notification service", new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 1, "Send Email" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Statistics page", new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 2, "Dashboard Report" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_attachments_task_id",
                table: "attachments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_uploaded_by",
                table: "attachments",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_comments_task_id",
                table: "comments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_user_id",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_members_project_id_user_id",
                table: "project_members",
                columns: new[] { "project_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_members_user_id",
                table: "project_members",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_projects_owner_id",
                table: "projects",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_assignee_id",
                table: "tasks",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_project_id",
                table: "tasks",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "project_members");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
