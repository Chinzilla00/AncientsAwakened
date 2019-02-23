using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.DevTools
{
	public class DevRadar : ModItem
	{
		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Chest Finder");
            BaseUtility.AddTooltips(item, new string[] { "Lights up all chests on the map" });			
		}

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 38;
            item.rare = 2;

            item.useStyle = 5;
            item.useAnimation = 50;
            item.useTime = 50;
            //item.UseSound = mod.SoundItem("LiquidRadarUse");			
        }

        public override bool UseItem(Player p)
        {
            if (Main.myPlayer == p.whoAmI && Main.netMode != 2)
            {
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        Tile tile = Main.tile[i, j];
                        if (WorldGen.InWorld(i, j) && Main.tileContainer[tile.type] == true)
                            Main.Map.Update(i, j, 255);
                    }
                }
            }
            return true;
		}

		public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, false, false); }
        public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}