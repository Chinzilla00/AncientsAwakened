using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class DragonScale : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Scale");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.maxStack = 99;
            item.rare = ItemRarityID.Blue;
        }
    }
}