using System;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class TreasureRadar : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Treasure Hunter");
            BaseUtility.AddTooltips(item, new string[] { "200 Tile Range", "Lights up chests on the map" });			
		}

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.rare = ItemRarityID.Green;
            item.value = BaseUtility.CalcValue(0, 2, 0, 0);

            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 50;
            item.useTime = 50;
            //item.UseSound = mod.SoundItem("LiquidRadarUse");			
        }

        public override bool UseItem(Player p)
        {
            if (Main.myPlayer == p.whoAmI && Main.netMode != 2)
            {
                int cX = (int)(p.Center.X / 16f); int cY = (int)(p.Center.Y / 16f);
                int range = 200;
                int topX = Math.Max(10, cX - range);
                int topY = Math.Max(10, cY - range);
                int bottomX = Math.Min(Main.maxTilesX, cX + range);
                int bottomY = Math.Min(Main.maxTilesY, cY + range);
                bool updateMap = false;
                for (int x = topX; x < bottomX; x++)
                {
                    for (int y = topY; y < bottomY; y++)
                    {
                        if (Main.tile[x, y] == null) { continue; }
                        Tile tile = Main.tile[x, y];
                        if (tile.active() && (Main.tileContainer[tile.type] == true))
                        {
                            if (Main.Map.UpdateLighting(x, y, Math.Max(Main.Map[x, y].Light, (byte)255))) updateMap = true;
                        }
                    }
                }
                if (updateMap)
                {
                    Main.mapMinX = topX; Main.mapMinY = topY;
                    Main.mapMaxX = bottomX; Main.mapMaxY = bottomY;
                    Main.updateMap = Main.refreshMap = true;
                }
            }
            return true;
		}

		public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, false, false); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}