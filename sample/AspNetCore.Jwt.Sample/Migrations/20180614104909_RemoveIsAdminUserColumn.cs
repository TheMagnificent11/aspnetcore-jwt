using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Jwt.Sample.Migrations
{
    /// <summary>
    /// Remove IsAdmin User Column Database Migration
    /// </summary>
    public partial class RemoveIsAdminUserColumn : Migration
    {
        /// <summary>
        /// Upgrades database
        /// </summary>
        /// <param name="migrationBuilder">Migration builder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");
        }

        /// <summary>
        /// Downgrades database
        /// </summary>
        /// <param name="migrationBuilder">Migration builder</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
