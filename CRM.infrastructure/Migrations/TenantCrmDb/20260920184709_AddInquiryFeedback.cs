using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.infrastructure.Migrations.TenantCrmDb
{
    /// <inheritdoc />
    public partial class AddInquiryFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Feedbacks");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Feedbacks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Feedbacks");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Feedbacks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
