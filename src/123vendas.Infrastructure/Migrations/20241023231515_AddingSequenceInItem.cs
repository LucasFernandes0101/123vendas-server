using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _123vendas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingSequenceInItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Sequence",
                table: "SaleItem",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "SaleItem");
        }
    }
}
