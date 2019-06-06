using System;
using System.Threading;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Worldgen
{
    class ConversionHandler
	{
		public static int CONVERTID_MIRE = 0;
		public static int CONVERTID_INFERNO = 1;
	
		public static int startMireX = -1;
		public static int startMireY = -1;
		public static int genMireWidth = -1;

		public static int startInfernoX = -1;
		public static int startInfernoY = -1;
		public static int genInfernoWidth = -1;

		public static void ConvertDown(int centerX, int y, int width, int convertType)
		{
			int worldSize = GetWorldSize();
			int biomeRadius = (worldSize == 3 ? 220 : worldSize == 2 ? 180 : 150);	
			biomeRadius /= 2;
			switch(convertType)
			{
				case 0: //MIRE
				{
					startMireX = centerX;
					startMireY = y;
					genMireWidth = width;
					ThreadPool.QueueUserWorkItem(new WaitCallback(ConvertDownMireCallback), null);
					break;
				}
				case 1: //INFERNO
				{
					startInfernoX = centerX;
					startInfernoY = y;
					genInfernoWidth = width;
					ThreadPool.QueueUserWorkItem(new WaitCallback(ConvertDownInfernoCallback), null);
					break;
				}				
			}
		}

        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }			

	#region thread callback stuff
		public static void ConvertDownMireCallback(object threadContext)
		{
			try
			{
				do_ConvertDownMire(threadContext);				
			}
			catch (Exception)
            {
			}
		}

		public static void do_ConvertDownMire(object threadContext)
		{
			dodo_ConvertDown(startMireX, startMireY, genMireWidth, 0);
		}

		public static void ConvertDownInfernoCallback(object threadContext)
		{
			try
			{
				do_ConvertDownInferno(threadContext);			
			}
			catch (Exception)
			{
			}
		}

		public static void do_ConvertDownInferno(object threadContext)
		{
			dodo_ConvertDown(startInfernoX, startInfernoY, genInfernoWidth, 1);
		}		
	#endregion	
	
		public static void dodo_ConvertDown(int startX, int startY, int genWidth, int conversionType)
		{
			Mod mod = AAMod.instance;
			int tileGrass = 0, wallGrass = 0, tileStone = 0, wallStone = 0, tileSand = 0, tileSandHard = 0, wallSandHard = 0, tileSandstone = 0, wallSandstone = 0, tileIce = 0, tileThorn = 0;
			switch(conversionType) //CHANGE THESE TO WHAT YOU WANT
			{
				case 0: //MIRE
				{
					tileGrass = mod.TileType("MireGrass");
					wallGrass = mod.WallType("MireJungleWall");
					tileStone = mod.TileType("Depthstone");
					wallStone = mod.WallType("DepthstoneWall");	
					tileSand = mod.TileType("Depthsand");
					tileSandHard = mod.TileType("DepthsandHardened");
                    wallSandHard = mod.WallType("DepthsandHardenedWall");
                    tileSandstone = mod.TileType("Depthsandstone");
					wallSandstone = mod.WallType("DepthsandstoneWall");	
					tileIce = mod.TileType("DepthIce");
					//tileThorn = mod.TileType("MireThorn");			
					break;
				}
				case 1: //INFERNO
				{
					tileGrass = mod.TileType("InfernoGrass");
					wallGrass = mod.WallType("InfernoGrassWall");
					tileStone = mod.TileType("Torchstone");
					wallStone = mod.WallType("TorchstoneWall");	
					tileSand = mod.TileType("Torchand");
					tileSandHard = mod.TileType("TorchsandHardened");
                    wallSandHard = mod.WallType("TorchsandHardenedWall");
                    tileSandstone = mod.TileType("Infernosandstone");
                    wallSandstone = mod.WallType("InfernosandstoneWall");	
                    tileIce = mod.TileType("Torchice");
                    //tileThorn = mod.TileType("InfernoThorn");										
                    break;
				}
				default: break;
			}
			int centerX = startX, y = startY;
			//centerX -= (genWidth / 2);
			for(int x1 = 0; x1 < genWidth; x1++)
			{
				while(y < (Main.maxTilesY -50))
				{
					Convert(centerX + x1, y, genWidth, tileGrass, wallGrass, tileStone, wallStone, tileSand, tileSandHard, wallSandHard, tileSandstone, wallSandstone, tileIce, tileThorn);
					y += genWidth * 2;
				}
			}					
		}

		public static void Convert(int i, int j, int size, int tileGrass, int wallGrass, int tileStone, int wallStone, int tileSand, int tileSandHard, int wallSandHard, int tileSandstone, int wallSandstone, int tileIce, int tileThorn)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1))
					{
						int type = (int)Main.tile[k, l].type;
						int wall = (int)Main.tile[k, l].wall;
						if (wallGrass != 0 && WallID.Sets.Conversion.Grass[wall] && wall != wallGrass)
						{
							Main.tile[k, l].wall = (ushort)wallGrass;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (wallStone != 0 && WallID.Sets.Conversion.Stone[wall] && wall != wallStone)
						{
							Main.tile[k, l].wall = (ushort)wallStone;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (wallSandHard != 0 && WallID.Sets.Conversion.HardenedSand[wall] && wall != wallSandHard)
						{
							Main.tile[k, l].wall = (ushort)wallSandHard;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (wallSandstone != 0 && WallID.Sets.Conversion.Sandstone[wall] && wall != wallSandstone)
						{
							Main.tile[k, l].wall = (ushort)wallSandstone;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						if (tileStone != 0 && (Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != tileStone)
						{
							Main.tile[k, l].type = (ushort)tileStone;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileGrass != 0 && TileID.Sets.Conversion.Grass[type] && type != tileGrass)
						{
							Main.tile[k, l].type = (ushort)tileGrass;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileIce != 0 && TileID.Sets.Conversion.Ice[type] && type != tileIce)
						{
							Main.tile[k, l].type = (ushort)tileIce;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileSand != 0 && TileID.Sets.Conversion.Sand[type] && type != tileSand)
						{
							Main.tile[k, l].type = (ushort)tileSand;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileSandHard != 0 && TileID.Sets.Conversion.HardenedSand[type] && type != tileSandHard)
						{
							Main.tile[k, l].type = (ushort)tileSandHard;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileSandstone != 0 && TileID.Sets.Conversion.Sandstone[type] && type != tileSandstone)
						{
							Main.tile[k, l].type = (ushort)tileSandstone;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileThorn != 0 && TileID.Sets.Conversion.Thorn[type] && type != tileThorn)
						{
							Main.tile[k, l].type = (ushort)tileThorn;
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
					}
				}
			}
		}
	}
}