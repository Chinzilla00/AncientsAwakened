using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Apawn
{
    public class ApawnEgg : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surprise Egg");
            Tooltip.SetDefault("<right> to open \n'Its a plastic egg. A REEEEEEEALLY big one.!'");
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
			player.QuickSpawnItem(ModContent.ItemType<ApawnHelm>());
            player.QuickSpawnItem(ModContent.ItemType<ApawnPlate>());
            player.QuickSpawnItem(ModContent.ItemType<ApawnBoots>());
        }
    }
}