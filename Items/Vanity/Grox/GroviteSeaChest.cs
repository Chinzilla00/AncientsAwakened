using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Grox
{
    public class GroviteSeaChest : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grovite Sea Chest");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Angry Code Pirate!'");
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
			player.QuickSpawnItem(ModContent.ItemType<AngryPirateHood>());
            player.QuickSpawnItem(ModContent.ItemType<AngryPirateCofferplate>());
            player.QuickSpawnItem(ModContent.ItemType<AngryPirateBoots>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<AngryPirateSails>());
            }
        }
    }
}