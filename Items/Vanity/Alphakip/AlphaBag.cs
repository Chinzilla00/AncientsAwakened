using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Alphakip
{
    public class AlphaBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mud Fish's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Fish King!'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true; item.expertOnly = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<FishDiverMask>());
            player.QuickSpawnItem(ModContent.ItemType<FishDiverJacket>());
            player.QuickSpawnItem(ModContent.ItemType<FishDiverBoots>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<KipronWings>());
            }
        }
    }
}