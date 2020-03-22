using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Blazen
{
    public class BlazenBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Lord's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Thunder Lord!'");
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
			player.QuickSpawnItem(ModContent.ItemType<BlazenHelmet>());
            player.QuickSpawnItem(ModContent.ItemType<BlazenPlate>());
            player.QuickSpawnItem(ModContent.ItemType<BlazenBoots>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<BlazenBooster>());
            }
        }
    }
}