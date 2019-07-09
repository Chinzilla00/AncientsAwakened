using Terraria;

namespace AAMod.Items.Boss.Toad
{
    public class ToadBag : BaseAAItem
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
			item.expert = true;
		}

        public override int BossBagNPC => mod.NPCType("TruffleToad");

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ToadMask"));
            }
            if (Main.rand.Next(20) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.HMDevArmor();
            }
            string[] lootTable = { "MushrockStaff", "ToadTongue", "Todegun" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            player.QuickSpawnItem(mod.ItemType("ToadLeg"));
        }
	}
}