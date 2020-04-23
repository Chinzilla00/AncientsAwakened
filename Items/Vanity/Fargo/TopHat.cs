using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Vanity.Fargo
{
    public class TopHat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirrelly Top Hat");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Meme Squirrel!'");
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
            player.QuickSpawnItem(ModContent.ItemType<FargoHat>());
            player.QuickSpawnItem(ModContent.ItemType<FargoSuit>());
            player.QuickSpawnItem(ModContent.ItemType<FargoPants>());
        }
    }
}