using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class DecayPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Warden");
            Tooltip.SetDefault("'That old spectre...what is he planning..?'");
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
            item.createTile = mod.TileType("DecayPainting");
        }
    }
}