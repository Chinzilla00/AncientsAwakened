using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Orthrus
{
    public class OrthrusBag : ModItem
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
			bossBagNPC = mod.NPCType("Orthrus");
		}

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("OrthrusMaskRed"));
            }
            else if (Main.rand.Next(7) == 1)
            {
                player.QuickSpawnItem(mod.ItemType("OrthrusMaskBlue"));
            }
            if (Main.rand.Next(20) == 1)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.HMDevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("FulguriteBar"), Main.rand.Next(40, 76));
            player.QuickSpawnItem(mod.ItemType("StormPendant"));
            player.QuickSpawnItem(Terraria.ID.ItemID.SoulofMight, Main.rand.Next(25, 40));
        }
	}
}