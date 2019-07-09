using Terraria;

namespace AAMod.Items.Boss.Truffle
{
    public class TruffleBag : BaseAAItem
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
        }
        public override int BossBagNPC => mod.NPCType("TechnoTruffle");

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("TruffleMask"));
            }
            if (Main.rand.Next(20) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.HMDevArmor();
            }
            player.QuickSpawnItem(Terraria.ID.ItemID.SoulofSight, Main.rand.Next(25, 40));
            player.QuickSpawnItem(mod.ItemType("FulguriteBar"), Main.rand.Next(40, 76));
            player.QuickSpawnItem(mod.ItemType("MetallicTruffle"));
		}
	}
}