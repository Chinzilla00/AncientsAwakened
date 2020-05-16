using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class GreedKey : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilded Key");
			Tooltip.SetDefault("This probably unlocks...something?");
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
