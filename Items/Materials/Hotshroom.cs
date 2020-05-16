using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class Hotshroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Mushroom");
            Tooltip.SetDefault("Only grows during the day");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
			item.maxStack = 99;
            item.rare = ItemRarityID.Blue;
        }
    }
}
