using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Grips
{
    public class GripBag : BaseAAItem
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
			item.rare = ItemRarityID.Cyan;
			item.expert = true; item.expertOnly = true;
        }
        public override int BossBagNPC => mod.NPCType("GripOfChaosBlue");

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("GripMaskBlue"));
            }
            else if (Main.rand.Next(7) == 1)
            {
                player.QuickSpawnItem(mod.ItemType("GripMaskRed"));
            }
            if (Main.rand.Next(10) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                modPlayer.PHMDevArmor();
            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("ClawBaton"));
            }
            player.QuickSpawnItem(mod.ItemType("Abyssium"), Main.rand.Next(25, 56));
            player.QuickSpawnItem(mod.ItemType("Incinerite"), Main.rand.Next(25, 56));
            player.QuickSpawnItem(mod.ItemType("ClawOfChaos"));
		}
	}
}