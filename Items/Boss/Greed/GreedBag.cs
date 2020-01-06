using Terraria;

namespace AAMod.Items.Boss.Greed
{
    public class GreedBag : BaseAAItem
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
        public override int BossBagNPC => mod.NPCType("Greed");

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
                modPlayer.PPDevArmor();
            }
            string[] lootTable = { "GildedGlock", "Miner", "StoneSlammer", "GoldDigger"};
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            player.QuickSpawnItem(mod.ItemType("CovetiteCoin"), Main.rand.Next(60, 150));
            player.QuickSpawnItem(mod.ItemType("DesireCharm"));
        }
	}
}