using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Charlie
{
    public class CharlieBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaper's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Grim Edgelord!'");
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
			player.QuickSpawnItem(ModContent.ItemType<CharlieCowl>());
            player.QuickSpawnItem(ModContent.ItemType<CharlieCloak>());
            player.QuickSpawnItem(ModContent.ItemType<CharlieBoots>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<CharlieWings>());
            }
        }
    }
}