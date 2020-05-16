using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class Timecaller : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Timecaller");
            Tooltip.SetDefault(@"Brings forth the next light.
Non-Consumable");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (Main.fastForwardTime)
            {
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (!Main.dayTime)
            {
                Main.dayTime = true;
                Main.time = 0;
            }
            else
            {
                Main.dayTime = false;
                Main.time = 0;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Suncaller", 1);
            recipe.AddIngredient(null, "Mooncaller", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
