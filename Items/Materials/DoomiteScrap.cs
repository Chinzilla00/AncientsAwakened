using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class DoomiteScrap : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Scrap");
            Tooltip.SetDefault(@"It's worthless
...or is it?");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = -1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("DoomitePlate");
        }
    }
}