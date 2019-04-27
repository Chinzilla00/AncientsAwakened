using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using AAMod.NPCs.Bosses.MushroomMonarch;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class PlanteraFlower : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Orchid");
            Tooltip.SetDefault(@"Summons Plantera
Can only be used in the underground jungle");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = 6;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ZoneJungle && player.Center.Y >= (Main.worldSurface * 16) + 800f && !NPC.AnyNPCs(NPCID.Plantera))
            {
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JungleSpores, 10);
                recipe.AddIngredient(ItemID.SoulofFright, 5);
                recipe.AddIngredient(ItemID.SoulofMight, 5);
                recipe.AddIngredient(ItemID.SoulofSight, 5);
                recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }

            {
                ModRecipe recipe = new ModRecipe(mod);

                recipe.AddIngredient(mod, "PlanteraPetal", 10);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this, 1);
                recipe.AddRecipe();
            }
        }
    }
}