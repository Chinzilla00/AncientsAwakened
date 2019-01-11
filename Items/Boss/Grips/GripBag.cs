using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    public class GripBag : ModItem
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
			bossBagNPC = mod.NPCType("GripOfChaosBlue");
		}

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
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("GripMaskRed"));
            }
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
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