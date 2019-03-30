using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Worldgeneration;

namespace AAMod.Items.DevTools
{
	public class Minilake : ModItem
    {
        private int mireSide = 0;

        private Vector2 mirePos = new Vector2(0, 0);

        private int infernoSide = 0;

        private Vector2 infernoPos = new Vector2(0, 0);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Lake");

            Tooltip.SetDefault(@"Spawns a Mire somewhere on the jungle side of your world
You have this item because either
A: This is an old world w/o AA worldgen in it
B: The thing this item is for failed to spawn.
No need to worry. Just use this item and you'll be good as new.
Careful though, this may lag your game for a few moments.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 10;
            item.value = BaseMod.BaseUtility.CalcValue(0, 0, 0, 0);

			item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.autoReuse = false;
            item.consumable = true;	
        }

		public override bool UseItem(Player player)
		{
            infernoSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
            infernoPos.X = ((Main.maxTilesX >= 8000) ? (infernoSide == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
            mirePos.X = ((Main.maxTilesX >= 8000) ? (infernoSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700))));
            
            int q = (int)WorldGen.worldSurfaceLow - 30;
            while (Main.tile[(int)(mirePos.X), q] != null && !Main.tile[(int)(mirePos.X), q].active())
            {
                q++;
            }
            for (int l = (int)(mirePos.X) - 25; l < (int)(mirePos.X) + 25; l++)
            {
                for (int m = q - 6; m < q + 90; m++)
                {
                    if (Main.tile[l, m] != null && Main.tile[l, m].active())
                    {
                        int type = Main.tile[l, m].type;
                        if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                        {
                            q++;
                        }
                    }
                }
            }
            mirePos.Y = q;
            Point origin = new Point((int)mirePos.X, (int)mirePos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            MireDelete delete = new MireDelete();
            MireBiome biome = new MireBiome();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
            return true;
        }

		public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}