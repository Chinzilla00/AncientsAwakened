using Terraria.ModLoader;

using Terraria.ID;

namespace AAMod.Items.Usable
{
    class LivingRazeleafWand : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 32;
            item.height = 26;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = false;
            item.createTile = mod.TileType("LivingRazeleaves");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Razeleaf Wand");
            Tooltip.SetDefault("Consumes Razewood");
        }

        public override bool UseItem(Terraria.Player player)
        {
            for (int num66 = 0; num66 < 58; num66++)
            {
                if (player.inventory[num66].type == mod.ItemType<Blocks.Razewood>() && player.inventory[num66].stack > 0)
                {
                    item.createTile = mod.TileType("LivingRazeleaves");
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
            recipe.AddIngredient(ItemID.LeafWand, 1);
            recipe.AddIngredient(null, "Razewood", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
