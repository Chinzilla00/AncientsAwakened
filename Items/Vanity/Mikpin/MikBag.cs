using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mikpin
{
    public class MikBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kitsune's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Weeb Fox!'");
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
			player.QuickSpawnItem(ModContent.ItemType<MikpinWig>());
            player.QuickSpawnItem(ModContent.ItemType<MikpinCloak>());
            player.QuickSpawnItem(ModContent.ItemType<MikpinPants>());
        }
    }
}