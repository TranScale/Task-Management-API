using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Users_UserId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Users_UserId",
                table: "TaskAssignments");

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectObjId", "EndDate", "IsDeleted", "ProjectDescription", "ProjectName", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, null, false, "Làm trang web bán hàng cho đối tác", "Dự án Website E-commerce", new DateTime(2026, 2, 13, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(3970), 1 },
                    { 2, null, false, "App quản lý nhân sự", "Dự án Mobile App React Native", new DateTime(2026, 3, 25, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(3976), 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, "Nguyễn Văn A (Manager)" },
                    { 2, "Trần Thị B (Developer)" },
                    { 3, "Lê Văn C (Tester)" }
                });

            migrationBuilder.InsertData(
                table: "ProjectMembers",
                columns: new[] { "ProjectMemberId", "MemberRole", "ProjectId", "UserId" },
                values: new object[,]
                {
                    { 1, "Project Manager", 1, 1 },
                    { 2, "Backend Developer", 1, 2 },
                    { 3, "Technical Lead", 2, 2 },
                    { 4, "QA/Tester", 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskObjId", "CreatedAt", "DueDate", "IsCompleted", "ProjectId", "TaskDescription", "TaskName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 15, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(4035), new DateTime(2026, 2, 23, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(4035), true, 1, "Vẽ sơ đồ ERD và viết script SQL", "Thiết kế Database" },
                    { 2, new DateTime(2026, 2, 24, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(4036), new DateTime(2026, 3, 20, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(4037), false, 1, "Sử dụng JWT để làm tính năng Authentication", "Code API Đăng nhập" },
                    { 3, new DateTime(2026, 3, 15, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(4037), new DateTime(2026, 3, 27, 4, 19, 57, 285, DateTimeKind.Utc).AddTicks(4038), false, 2, "Cài đặt Android Studio và Xcode", "Setup môi trường Mobile" }
                });

            migrationBuilder.InsertData(
                table: "TaskAssignments",
                columns: new[] { "TaskAssignmentId", "TaskId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Users_UserId",
                table: "ProjectMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Users_UserId",
                table: "TaskAssignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Users_UserId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Users_UserId",
                table: "TaskAssignments");

            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TaskAssignments",
                keyColumn: "TaskAssignmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskAssignments",
                keyColumn: "TaskAssignmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskAssignments",
                keyColumn: "TaskAssignmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskObjId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskObjId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskObjId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectObjId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectObjId",
                keyValue: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Users_UserId",
                table: "ProjectMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Users_UserId",
                table: "TaskAssignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
