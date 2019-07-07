
using Terraria;
using Terraria.ID;
using BaseMod;

namespace AAMod.Items.Usable
{
    public class MireSeeds : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dank Seeds");
            Tooltip.SetDefault("Plants Mire grass"); ;	
		}		
		
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 8;
            item.value = BaseUtility.CalcValue(0, 0, 0, 5);

            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.useTurn = true;
            item.createTile = mod.TileType("MireGrass");
            item.consumable = true;		
        }

		public override bool CanUseItem(Player p)
		{
			Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			if(tile != null && tile.active() && tile.type == TileID.Mud)
			{
				WorldGen.destroyObject = true;
				TileID.Sets.BreakableWhenPlacing[TileID.Mud] = true;
				return base.CanUseItem(p);				
			}
			return false;
		}

		public override bool UseItem(Player p)
		{
			WorldGen.destroyObject = false;
			TileID.Sets.BreakableWhenPlacing[TileID.Mud] = false;		
			return base.UseItem(p);
		}
	}
}