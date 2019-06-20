using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class DarkmatterOre : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = 10;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("RadiumOre"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Darkmatter Ore");
            Tooltip.SetDefault("It feels weightless, yet it still has some kind of mass to it");
        }

    }
}
