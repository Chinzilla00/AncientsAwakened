using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class DepthsandHardened : BaseAAItem
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
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Green;
            item.consumable = true;
            item.createTile = mod.TileType("DepthsandHardened"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hardened Depthsand");
        }

    }
}
