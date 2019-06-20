using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    class Bogwood : BaseAAItem
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
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("Bogwood"); //put your CustomBlock Tile name
            item.ammo = item.type;
            item.notAmmo = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood");
            Tooltip.SetDefault("");
        }
    }
}
