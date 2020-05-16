using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Paintings
{
    public class AkumaPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Demon");
            Tooltip.SetDefault("'The flames of hate...they still rage in this serpent...'");
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
            item.createTile = mod.TileType("AkumaPainting");
        }
    }
}