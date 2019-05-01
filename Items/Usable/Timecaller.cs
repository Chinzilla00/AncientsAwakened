using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Shen;
using System.Collections.Generic;
using BaseMod;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod.Items.Usable
{
    public class Timecaller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Timecaller");
            Tooltip.SetDefault(@"Brings forth the next light.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.rare = 8;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
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
                AAWorld.Mooncaller = true;
            }
            else
            {
                AAWorld.Suncaller = true;
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
