using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Tails
{
    public class TailsToolbox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tails' Toolbox");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Fox Wonder!'");
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
			player.QuickSpawnItem(ModContent.ItemType<TailsHead>());
            player.QuickSpawnItem(ModContent.ItemType<TailsBody>());
            player.QuickSpawnItem(ModContent.ItemType<TailsLegs>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.Jetpack);
            }
        }
    }
}