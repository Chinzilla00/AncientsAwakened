using Terraria;
using Terraria.ModLoader;
using AAMod.Items.Vanity.Mikpin.Kitsune;
using AAMod.Items.Vanity.Mikpin.Angel;

namespace AAMod.Items.Vanity.Mikpin
{
    public class MikBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Six-Star Operator Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Apple Pie-loving Sniper!'");
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
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<MikpinWig>());
                player.QuickSpawnItem(ModContent.ItemType<MikpinCloak>());
                player.QuickSpawnItem(ModContent.ItemType<MikpinPants>());
            }
            else
            {
                player.QuickSpawnItem(ModContent.ItemType<MikWig>());
                player.QuickSpawnItem(ModContent.ItemType<MikJacket>());
                player.QuickSpawnItem(ModContent.ItemType<MikLeggings>());
            }
        }
    }
}