using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class TorchAsh : BaseAAItem
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
            item.rare = 0;
            item.value = 0;
            item.consumable = true;
            item.createTile = mod.TileType("TorchAsh"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Ash");
        }
    }
}
