using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.infrastructure.Migrations.TenantCrmDb
{
    /// <inheritdoc />
    public partial class AddPromotionAndCampaignReasonAndAudience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Promotions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TargetAllMembers",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetCorporate",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetPWD",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetSenior",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetStudent",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Campaigns",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TargetAllMembers",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetCorporate",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetPWD",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetSenior",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TargetStudent",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "TargetAllMembers",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "TargetCorporate",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "TargetPWD",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "TargetSenior",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "TargetStudent",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "TargetAllMembers",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "TargetCorporate",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "TargetPWD",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "TargetSenior",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "TargetStudent",
                table: "Campaigns");
        }
    }
}
