using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Maskano
{
    public class MaskBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mask Bag");
            Tooltip.SetDefault(@"<right> to open
'All the essentials for impersonating the Mask Lord.'");
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
			player.QuickSpawnItem(ModContent.ItemType<Mask>());
            player.QuickSpawnItem(ModContent.ItemType<MaskPlate>());
            player.QuickSpawnItem(ModContent.ItemType<MaskBoots>());
        }
    }
}