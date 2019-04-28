using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Equinox;
using System.Collections.Generic;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class EquinoxWorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox Worm");
            Tooltip.SetDefault(@"A worm created using celestial materials
Summons the Equinox Worms");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.rare = 11;
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType<NightcrawlerHead>()) && !NPC.AnyNPCs(mod.NPCType<DaybringerHead>());
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(mod, player, "NightcrawlerHead", true, 1, 0, "The Equinox Worms", true);
            AAModGlobalNPC.SpawnBoss(mod, player, "DaybringerHead", false, -1, 0);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MechanicalWorm, 2);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}