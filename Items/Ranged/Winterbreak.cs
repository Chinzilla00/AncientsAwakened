using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Winterbreak : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Winterbreak");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {

            item.shoot = mod.ProjectileType("Winterbreak");
            item.shootSpeed = 10f;
            item.damage = 32;
            item.knockBack = 5f;
            item.ranged = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 24;
            item.useTime = 24;
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.consumable = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.value = 60;
            item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SnowMana");
            recipe.AddIngredient(ItemID.BorealWood, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
