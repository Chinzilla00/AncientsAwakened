using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Dallin
{
    public class FezLordsBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fez Lord's Bag");
            Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Fez Lord!'");
        }

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ItemID.Fez);	
			player.QuickSpawnItem(ItemID.TheDoctorsShirt);		
			player.QuickSpawnItem(ItemID.TheDoctorsPants);
			player.QuickSpawnItem(ItemID.ReflectiveDye, 3);			
        }
    }
}