using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Cerberus
{
    public class InvokerBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invoker's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Invoker of Pups!'");
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
			player.QuickSpawnItem(ModContent.ItemType<InvokerHood>());
            player.QuickSpawnItem(ModContent.ItemType<InvokerRobe>());
            player.QuickSpawnItem(ModContent.ItemType<InvokerPants>());
            player.QuickSpawnItem(ModContent.ItemType<Pets.CerberusWhistle>());
        }
    }
}