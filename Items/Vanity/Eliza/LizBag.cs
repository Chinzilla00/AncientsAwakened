using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Eliza
{
    public class LizBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Dragon's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dragon Queen!'");
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
            player.QuickSpawnItem(mod.ItemType("LizEars"));
            player.QuickSpawnItem(mod.ItemType("LizShirt"));
            player.QuickSpawnItem(mod.ItemType("LizBoots"));
            player.QuickSpawnItem(mod.ItemType("LizScarf"));
            player.QuickSpawnItem(mod.ItemType("RoyalStar"));
            player.QuickSpawnItem(ItemID.TwilightHairDye);

            if (Main.expertMode)
            {
                player.QuickSpawnItem(mod.ItemType("NightingaleWings"));
                player.QuickSpawnItem(ItemID.TwilightDye);
            }
        }
    }
}