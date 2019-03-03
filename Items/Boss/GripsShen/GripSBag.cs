using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.GripsShen
{
    public class GripSBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 36;
			item.height = 32;
			item.rare = 9;
			item.expert = true;
			bossBagNPC = mod.NPCType("BlazeGrip");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.PMLDevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("EventideAbyssiumOre"), Main.rand.Next(15, 26));
            player.QuickSpawnItem(mod.ItemType("DaybreakIncineriteOre"), Main.rand.Next(15, 26));
            player.QuickSpawnItem(mod.ItemType("DiscordianShredder"));

            if (Main.rand.Next(10) == 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    player.QuickSpawnItem(mod.ItemType("GripMaskAbyss"));
                }
                else
                {
                    player.QuickSpawnItem(mod.ItemType("GripMaskBlaze"));
                }
            }
        }
	}
}