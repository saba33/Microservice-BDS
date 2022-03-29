using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMSSenderManagement.Repository.Migrations
{
    public partial class AddedseveralInfoToentityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Otp",
                table: "OtpAndIds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OtpAndIds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OtpAndIds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmsProvider",
                table: "OtpAndIds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidateOn",
                table: "OtpAndIds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OtpAndIds");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OtpAndIds");

            migrationBuilder.DropColumn(
                name: "SmsProvider",
                table: "OtpAndIds");

            migrationBuilder.DropColumn(
                name: "ValidateOn",
                table: "OtpAndIds");

            migrationBuilder.AlterColumn<string>(
                name: "Otp",
                table: "OtpAndIds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
