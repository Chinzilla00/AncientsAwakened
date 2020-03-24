using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Delly
{
    public class DellyBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daughter of the Void's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Void Mistress!'");
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
			player.QuickSpawnItem(ModContent.ItemType<DellyWig>());
            player.QuickSpawnItem(ModContent.ItemType<DellyShirt>());
            player.QuickSpawnItem(ModContent.ItemType<DellyBoots>());
        }
    }
}