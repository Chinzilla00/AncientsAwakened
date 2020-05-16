using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class RadiumOre : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Red;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("RadiumOre"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Ore");
            Tooltip.SetDefault("Twinkles like the stars in the midnight skies");
        }

    }
}
