﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
	public partial class Mig9 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "BlogRatings",
				columns: table => new
				{
					BlogRatingID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					BlogID = table.Column<int>(type: "int", nullable: false),
					BlogTotalRating = table.Column<int>(type: "int", nullable: false),
					BlogRatingCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_BlogRatings", x => x.BlogRatingID);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "BlogRatings");
		}
	}
}
