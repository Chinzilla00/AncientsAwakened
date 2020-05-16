
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class DoomSeeds : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Seeds");
            Tooltip.SetDefault("Plants Doomgrass"); ;	
		}		
		
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = ItemRarityID.Yellow;
            item.value = BaseUtility.CalcValue(0, 0, 0, 5);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.useTurn = true;
            item.createTile = mod.TileType("DoomGrass");
            item.consumable = true;		
        }

		public override bool CanUseItem(Player p)
		{
			Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			if(tile != null && tile.active() && tile.type == TileID.Dirt)
			{
				WorldGen.destroyObject = true;
				TileID.Sets.BreakableWhenPlacing[TileID.Dirt] = true;
				return base.CanUseItem(p);				
			}
			return false;
		}

		public override bool UseItem(Player p)
		{
			WorldGen.destroyObject = false;
			TileID.Sets.BreakableWhenPlacing[TileID.Dirt] = false;		
			return base.UseItem(p);
		}
	}
}