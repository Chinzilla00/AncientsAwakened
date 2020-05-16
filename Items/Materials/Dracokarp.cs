using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class Dracokarp : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dracokarp");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
        }
    }
}