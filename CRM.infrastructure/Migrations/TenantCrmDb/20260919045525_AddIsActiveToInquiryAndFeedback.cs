using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.infrastructure.Migrations.TenantCrmDb
{
    /// <inheritdoc />
    public partial class AddIsActiveToInquiryAndFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Inquiries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Feedbacks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Feedbacks");
        }
    }
}
