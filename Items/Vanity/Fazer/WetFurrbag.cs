using Terraria;

namespace AAMod.Items.Vanity.Fazer
{
    public class WetFurrbag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wet Furrbag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Funloving Fox!'");
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
            player.QuickSpawnItem(mod.ItemType("SammyWig"));
            player.QuickSpawnItem(mod.ItemType("SammySweater"));
            player.QuickSpawnItem(mod.ItemType("SammyPants"));
        }
    }
}