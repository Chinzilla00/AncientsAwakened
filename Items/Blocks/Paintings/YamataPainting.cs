using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class YamataPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("7-Headed Scream");
            Tooltip.SetDefault("'Sweet Azathoth, will this oversized lizard zip his lip?!'");
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
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.createTile = mod.TileType("YamataPainting");
        }
    }
}