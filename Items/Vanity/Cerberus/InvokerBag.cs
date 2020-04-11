using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Cerberus
{
    public class InvokerBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pup Cerberus' Kennel");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Invoker of Pups!'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true; item.expertOnly = true;
            item.createTile = mod.TileType("CerberusKennel"); 
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ModContent.ItemType<InvokerHood>());
            player.QuickSpawnItem(ModContent.ItemType<InvokerRobe>());
            player.QuickSpawnItem(ModContent.ItemType<InvokerPants>());
            player.QuickSpawnItem(ModContent.ItemType<Pets.CerberusWhistle>());
        }
    }
}