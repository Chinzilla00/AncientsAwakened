using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class DragonBell : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Bell");
            Tooltip.SetDefault(@"An ornately crafted bell
Summons the Broodmother in the Inferno
Only useable during the day");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 38;
            item.maxStack = 20;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Broodmother"), true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.Broodmother"), false);
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DragonBellDayTimeFalse"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if (NPC.AnyNPCs(mod.NPCType("Broodmother")))
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DragonBellTrue"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DragonBellFalse"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonScale", 15);
            recipe.AddIngredient(null, "Sunpowder", 30);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}