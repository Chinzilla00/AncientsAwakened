using Terraria;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class GreedABag : BaseAAItem
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
			item.width = 32;
			item.height = 36;
			item.rare = 11;
			item.expert = true; item.expertOnly = true;
        }
        public override int BossBagNPC => mod.NPCType("GreedA");

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("GreedMask"));
            }
            if (Main.rand.Next(20) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PMLDevArmor();
            }
            string[] lootTable = { "OreCannon", "Unearther", "OreStaff", "Earthbreaker" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            player.QuickSpawnItem(mod.ItemType("WormIdol"));
            player.QuickSpawnItem(mod.ItemType("DesireTalisman"));
        }
	}
}