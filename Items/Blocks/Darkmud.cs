using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class Darkmud : BaseAAItem
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
            item.rare = ItemRarityID.Blue;
            item.consumable = true;
            item.createTile = mod.TileType("Darkmud"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmud");
        }
    }
}
