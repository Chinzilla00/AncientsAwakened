using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Vanity.CC
{
    public class CCBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Manic's Cardboard Box");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dread Devotee!'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true; item.expertOnly = true;
            item.createTile = mod.TileType("CCMireBox"); 
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(ModContent.ItemType<Shiny.ShinyCCHood>());
                player.QuickSpawnItem(ModContent.ItemType<Shiny.ShinyCCRobe>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(ModContent.ItemType<Accessories.Wings.MagmancerWings>());
                }
                return;
            }
            player.QuickSpawnItem(ModContent.ItemType<CCHood>());
            player.QuickSpawnItem(ModContent.ItemType<CCRobe>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ModContent.ItemType<Accessories.Wings.AquamancerWings>());
            }
        }
    }
}