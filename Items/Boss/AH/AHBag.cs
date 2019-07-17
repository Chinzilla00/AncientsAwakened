using Terraria;

namespace AAMod.Items.Boss.AH
{
    public class AHBag : BaseAAItem
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
            item.expert = true; item.expertOnly = true;
		}

        public override int BossBagNPC => mod.NPCType("Ashe");

        public override bool CanRightClick()
		{
			return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(20) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.PMLDevArmor();
            }

            string[] lootTableA = { "AshRain", "FuryFlame", "FireSpiritStaff", "AsheSatchel" };
            int lootA = Main.rand.Next(lootTableA.Length);
            player.QuickSpawnItem(mod.ItemType(lootTableA[lootA]));

            string[] lootTableH = { "HarukaKunai", "Masamune", "MizuArashi", "HarukaBox" };
            int lootH = Main.rand.Next(lootTableH.Length);
            player.QuickSpawnItem(mod.ItemType(lootTableH[lootH]));


            player.QuickSpawnItem(mod.ItemType("HeartOfPassion"));
            player.QuickSpawnItem(mod.ItemType("HeartOfSorrow"));
        }
	}
}