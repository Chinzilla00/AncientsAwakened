using Terraria;

namespace AAMod.Items.Boss.Shen
{
    public class ShenCache : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Cache");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 36;
			item.height = 32;
			item.expert = true; item.expertOnly = true;
		}

        public override int BossBagNPC => mod.NPCType("ShenA");

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("ChaosScale"), Main.rand.Next(30, 40));
            player.QuickSpawnItem(mod.ItemType("ChaosSoul"));
            player.QuickSpawnItem(mod.ItemType("EXSoul"));
            string[] lootTable = 
            {
                "ChaosSlayer", "MeteorStrike", "Skyfall", "Astroid", "DraconicRipper", "FlamingTwilight", "ShenTerratool", "Timesplitter"
            };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
        }
	}
}