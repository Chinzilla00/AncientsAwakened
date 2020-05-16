using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class HydraToxin : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogtoxin");
            Tooltip.SetDefault("Exceedingly corrosive venom.");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 22;
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
            item.value = 900;
        }
    }
}