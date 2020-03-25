using Terraria;
using Terraria.ModLoader;
using AAMod.Items.Vanity.Moon.Shiny;

namespace AAMod.Items.Vanity.Moon
{
    public class MoonBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Insect's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Moon Bee!'");
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
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<MoonWings>());
            }
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(ModContent.ItemType<ShinyMoonHood>());
                player.QuickSpawnItem(ModContent.ItemType<ShinyMoonRobe>());
                player.QuickSpawnItem(ModContent.ItemType<ShinyMoonBoots>());
                return;
            }
			player.QuickSpawnItem(ModContent.ItemType<MoonHood>());
            player.QuickSpawnItem(ModContent.ItemType<MoonRobe>());
            player.QuickSpawnItem(ModContent.ItemType<MoonBoots>());
        }
    }
}