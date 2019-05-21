using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class Shroomplant : ModItem
	{

		public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.StrangePlant1);
            item.createTile = mod.TileType<Tiles.Shroomplant>();
        }

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(@"The Mushman may want this.");
		}
    }
}
