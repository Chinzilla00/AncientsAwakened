using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    class Razewood : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 24;
            item.height = 22;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("Razewood"); //put your CustomBlock Tile name
            item.ammo = item.type;
            item.notAmmo = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood");
            Tooltip.SetDefault("");
        }
    }
}
