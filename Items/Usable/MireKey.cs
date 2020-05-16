using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class MireKey : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mire Key");
			Tooltip.SetDefault("'Unlocks the power of the wrathful abyss'");
		}


        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = ItemRarityID.LightPurple;
            item.maxStack = 99;
			item.value = 800000;
            item.noMelee = true;
        }

       
    }
}
