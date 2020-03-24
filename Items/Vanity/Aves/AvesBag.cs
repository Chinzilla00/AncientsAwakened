using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Aves
{
    public class AvesBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DJ Duck's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Monochrome Mallard!'");
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
			player.QuickSpawnItem(ModContent.ItemType<DJDuckHead>());
            player.QuickSpawnItem(ModContent.ItemType<DJDuckShirt>());
            player.QuickSpawnItem(ModContent.ItemType<DJDuckPants>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<DuckstepWings>());
            }
        }
    }
}