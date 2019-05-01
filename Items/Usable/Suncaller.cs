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
    public class Suncaller : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suncaller");
            Tooltip.SetDefault(@"Brings forth the morning sun.");
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
            if (Main.dayTime || Main.fastForwardTime)
            {
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            Main.dayTime = true;
            Main.time = 0;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiantIncinerite", 10);
            recipe.AddIngredient(null, "SoulOfSmite", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
