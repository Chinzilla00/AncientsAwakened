using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class SanguinePainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood and Bones");
            Tooltip.SetDefault("'They seem to have taken the term 'face monster' a bit too seriously.'");
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
            item.createTile = mod.TileType("SanguinePainting");
        }
    }
}