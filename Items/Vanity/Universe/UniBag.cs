using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Vanity.Universe
{
    public class UniBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Geode");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the whole universe!'");
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
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(ModContent.ItemType<Lemon>());
            }
            else
            {
                player.QuickSpawnItem(ModContent.ItemType<UniHead>());
            }
            player.QuickSpawnItem(ModContent.ItemType<UniSweater>());
            player.QuickSpawnItem(ModContent.ItemType<UniPants>());
        }
    }
}