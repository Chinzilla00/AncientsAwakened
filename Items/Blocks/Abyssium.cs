using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class Abyssium : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 1;
            item.value = Terraria.Item.sellPrice(0, 0, 8, 0);
            item.consumable = true;
            item.createTile = mod.TileType("AbyssiumOre");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssium");
            Tooltip.SetDefault("It's all mushy. Nasty.");
        }
    }
}
