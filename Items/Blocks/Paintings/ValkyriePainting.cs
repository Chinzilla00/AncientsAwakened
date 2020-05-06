using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class ValkyriePainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valor");
            Tooltip.SetDefault("'I hope you can find your lost loved one...'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.createTile = mod.TileType("ValkyriePainting");
        }
    }
}