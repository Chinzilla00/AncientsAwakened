using Terraria;
using Terraria.ID;

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
			item.rare = ItemRarityID.Purple;
			item.expert = true; item.expertOnly = true;
        }
        public override int BossBagNPC => mod.NPCType("Greed");

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("StoneShell"), Main.rand.Next(25, 30));
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("GreedMask"));
            }
            if (Main.rand.Next(10) == 0)
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