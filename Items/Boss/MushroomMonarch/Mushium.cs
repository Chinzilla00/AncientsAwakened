using Terraria.ID;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class Mushium : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = ItemRarityID.Blue;
            item.value = Terraria.Item.sellPrice(0, 0, 3, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushium");
        }
    }
}
