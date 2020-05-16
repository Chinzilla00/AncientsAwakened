using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class SwampKoi : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Swamp Koi");
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