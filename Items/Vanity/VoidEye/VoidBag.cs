using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.VoidEye
{
    public class VoidBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Eye Bag");
            Tooltip.SetDefault(@"<right> to open
'All the essentials for impersonating the Dark Watcher!'");
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
			player.QuickSpawnItem(ModContent.ItemType<VoidEyeHelm>());
            player.QuickSpawnItem(ModContent.ItemType<VoidEyePlate>());
            player.QuickSpawnItem(ModContent.ItemType<VoidEyeBoots>());
        }
    }
}