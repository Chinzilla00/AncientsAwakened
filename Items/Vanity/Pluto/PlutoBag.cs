using Terraria;
using Terraria.ModLoader;
using AAMod.Items.Vanity.Pluto.Shiny;

namespace AAMod.Items.Vanity.Pluto
{
    public class PlutoBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Outer God's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dwarf God!'");
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
                player.QuickSpawnItem(ModContent.ItemType<ShinyPlutoMask>());
                player.QuickSpawnItem(ModContent.ItemType<ShinyPlutoPlate>());
                player.QuickSpawnItem(ModContent.ItemType<PlutoBoots>());
                if (Main.hardMode)
                {
                    //player.QuickSpawnItem(ModContent.ItemType<>());
                }
                return;
            }
			player.QuickSpawnItem(ModContent.ItemType<PlutoMask>());
            player.QuickSpawnItem(ModContent.ItemType<PlutoPlate>());
            player.QuickSpawnItem(ModContent.ItemType<PlutoBoots>());
            if (Main.hardMode)
            {
                //player.QuickSpawnItem(ModContent.ItemType<>());
            }
        }
    }
}