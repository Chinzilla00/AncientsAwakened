using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class Darkshroom : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Mushroom");
            Tooltip.SetDefault("Only grows at night");
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
