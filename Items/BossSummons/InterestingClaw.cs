using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class InterestingClaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Interesting Looking Claw");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"It's oddly Clammy
Can only be used at night");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 24;
			item.maxStack = 20;
			item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HydraClaw", 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<GripOfChaosBlue>()) || NPC.AnyNPCs(ModContent.NPCType<GripOfChaosRed>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.InterestingClawFalse1"), Color.Indigo, false);
                return false;
            }
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.InterestingClawFalse2"), Color.Indigo, false);
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (Main.netMode == 0) { if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.GripsofChaosAwoken"), 175, 75, 255, false); }
            else if (Main.netMode == 2)
            if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != 1) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.GripsofChaosAwoken"), 175, 75, 255, false); }
            else if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAMod.Common.GripsofChaosAwoken")), new Color(175, 75, 255), -1);
            }
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("GripOfChaosBlue"), false, 1, 0);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("GripOfChaosRed"), false, -1, 0);
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}