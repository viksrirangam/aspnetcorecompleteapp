using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blipkart.Migrations
{
    public partial class firstchng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItems");
        }
    }
}
