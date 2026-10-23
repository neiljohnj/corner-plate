using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CornerPlate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMenuCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_categories", x => x.id);
                    table.CheckConstraint("ck_menu_category_nonnegative_sort_order", "sort_order >= 0");
                });

            migrationBuilder.CreateTable(
                name: "menu_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    product_type = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    current_price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    tax_code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_products", x => x.id);
                    table.CheckConstraint("ck_menu_product_name_not_empty", "btrim(name) <> ''");
                    table.CheckConstraint("ck_menu_product_nonnegative_price", "current_price >= 0");
                    table.CheckConstraint("ck_menu_product_strict_product_type", "product_type IN ('Single','Bundle')");
                    table.ForeignKey(
                        name: "fk_menu_products_menu_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "menu_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bundle_components",
                columns: table => new
                {
                    bundle_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    component_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bundle_components", x => new { x.bundle_product_id, x.component_product_id });
                    table.CheckConstraint("ck_bundle_component_bundle_product_not_equal_to_component_prod~", "bundle_product_id <> component_product_id");
                    table.CheckConstraint("ck_bundle_component_nonnegative_sort_order", "sort_order >= 0");
                    table.CheckConstraint("ck_bundle_component_positive_quantity", "quantity > 0");
                    table.ForeignKey(
                        name: "fk_bundle_components_menu_products_bundle_product_id",
                        column: x => x.bundle_product_id,
                        principalTable: "menu_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_bundle_components_menu_products_component_product_id",
                        column: x => x.component_product_id,
                        principalTable: "menu_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_choice_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    menu_product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    group_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    min_selections = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    max_selections = table.Column<int>(type: "integer", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_choice_groups", x => x.id);
                    table.CheckConstraint("ck_product_choice_group_max_selections_positive", "max_selections > 0");
                    table.CheckConstraint("ck_product_choice_group_min_less_than_or_equal_to_max", "min_selections <= max_selections");
                    table.CheckConstraint("ck_product_choice_group_min_selections_nonnegative", "min_selections >= 0");
                    table.CheckConstraint("ck_product_choice_group_name_not_empty", "btrim(name) <> ''");
                    table.CheckConstraint("ck_product_choice_group_nonnegative_sort_order", "sort_order >= 0");
                    table.CheckConstraint("ck_product_choice_group_type_valid", "group_type IN ('Choice', 'Substitution', 'AddOn')");
                    table.ForeignKey(
                        name: "fk_product_choice_groups_menu_products_menu_product_id",
                        column: x => x.menu_product_id,
                        principalTable: "menu_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_choice_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    choice_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    component_product_id = table.Column<Guid>(type: "uuid", nullable: true),
                    label = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    price_adjustment = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false, defaultValue: 0m),
                    max_quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    is_default = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_choice_options", x => x.id);
                    table.UniqueConstraint("uq_product_choice_options_group_id", x => new { x.choice_group_id, x.id });
                    table.CheckConstraint("ck_product_choice_option_label_not_empty", "btrim(label) <> ''");
                    table.CheckConstraint("ck_product_choice_option_nonnegative_price_adjustment", "price_adjustment >= 0");
                    table.CheckConstraint("ck_product_choice_option_nonnegative_sort_order", "sort_order >= 0");
                    table.CheckConstraint("ck_product_choice_option_positive_max_quantity", "max_quantity > 0");
                    table.ForeignKey(
                        name: "fk_product_choice_options_menu_products_component_product_id",
                        column: x => x.component_product_id,
                        principalTable: "menu_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_product_choice_options_product_choice_groups_choice_group_id",
                        column: x => x.choice_group_id,
                        principalTable: "product_choice_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bundle_components_component_product_id",
                table: "bundle_components",
                column: "component_product_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_categories_name",
                table: "menu_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_menu_products_category_id",
                table: "menu_products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_products_category_id_is_active_is_available",
                table: "menu_products",
                columns: new[] { "category_id", "is_active", "is_available" });

            migrationBuilder.CreateIndex(
                name: "ix_menu_products_product_type_is_active",
                table: "menu_products",
                columns: new[] { "product_type", "is_active" });

            migrationBuilder.CreateIndex(
                name: "ix_product_choice_groups_menu_product_id_name",
                table: "product_choice_groups",
                columns: new[] { "menu_product_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_choice_options_choice_group_id_label",
                table: "product_choice_options",
                columns: new[] { "choice_group_id", "label" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_choice_options_component_product_id",
                table: "product_choice_options",
                column: "component_product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bundle_components");

            migrationBuilder.DropTable(
                name: "product_choice_options");

            migrationBuilder.DropTable(
                name: "product_choice_groups");

            migrationBuilder.DropTable(
                name: "menu_products");

            migrationBuilder.DropTable(
                name: "menu_categories");
        }
    }
}
