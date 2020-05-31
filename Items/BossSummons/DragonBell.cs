
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
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"An ornately crafted bell
Summons the Broodmother in the Inferno
Only useable during the day");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 38;
            item.maxStack = 20;
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            Globals.AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Broodmother"), true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.Broodmother"), false);
            Main.PlaySound(SoundID.Roar, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DragonBellDayTimeFalse"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if (NPC.AnyNPCs(mod.NPCType("Broodmother")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DragonBellFalse1"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.DragonBellFalse2"), Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
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