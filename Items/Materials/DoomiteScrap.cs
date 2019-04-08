using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class DoomiteScrap : ModItem
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
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("DoomitePlate");
        }
    }
}