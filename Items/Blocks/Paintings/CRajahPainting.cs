using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class CRajahPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Champion of the Innocent");
            Tooltip.SetDefault("'The only thing more powerful than a defender is his unending fury.'");
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
            item.createTile = mod.TileType("CRajahPainting");
        }
    }
}