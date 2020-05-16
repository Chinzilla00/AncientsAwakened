
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class MyceliumSeeds : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mycelium Seeds");
            Tooltip.SetDefault("Plants Mycelium"); ;	
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
            item.createTile = mod.TileType("Mycelium");
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