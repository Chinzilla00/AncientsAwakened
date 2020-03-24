using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Vanity.Tied
{
    public class OldMagiciansHat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Magician's Top Hat");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Dapper Bone Man!'");
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
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(ItemID.GoldBunny);
                }
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    player.QuickSpawnItem(ItemID.Bunny);
                }
            }
			player.QuickSpawnItem(ModContent.ItemType<TiedsMask>());
            player.QuickSpawnItem(ModContent.ItemType<TiedsSuit>());
            player.QuickSpawnItem(ModContent.ItemType<TiedsLeggings>());
            if (Main.hardMode)
            {
                player.QuickSpawnItem(ItemID.BoneWings);
            }

        }
    }
}