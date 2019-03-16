using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    class LivingRazewoodWand : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 26;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = false;
            item.createTile = mod.TileType("LivingRazewood");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Razewood Wand");
            Tooltip.SetDefault("Consumes Razewood");
        }

        public override bool UseItem(Player player)
        {
            for (int num66 = 0; num66 < 58; num66++)
            {
                if (player.inventory[num66].type == mod.ItemType<Blocks.Razewood>() && player.inventory[num66].stack > 0)
                {
                    item.createTile = mod.TileType("LivingRazewood");
                    player.inventory[num66].stack -= 1;
                    return true;
                }
            }
            item.createTile = -1;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LivingWoodWand, 1);
            recipe.AddIngredient(null, "Razewood", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
