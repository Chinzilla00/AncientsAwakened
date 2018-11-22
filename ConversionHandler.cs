using System;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.UI;
using Terraria.GameContent.UI;

namespace AAMod.Worldgen
{
	class ConversionHandler
	{		
		public static int CONVERTID_MIRE = 0;
		public static int CONVERTID_INFERNO = 1;
		
		public static int startX = -1;
		public static int startY = -1;
		public static int genWidth = -1;
		public static int conversionType = 1;
		
		public static bool FULLBRIGHT_MAP = true; //causes the gen to be fullbright on the map for easy viewing

		//CALL THIS TO START CONVERTING
		//example use: ConversionHandler.ConvertDown(x, y, 10, ConversionHandler.CONVERTID_MIRE);
		public static void ConvertDown(int centerX, int y, int width, int convertType)
		{
			startX = centerX;
			startY = y;
			genWidth = width;
			conversionType = convertType;
			ThreadPool.QueueUserWorkItem(new WaitCallback(ConversionHandler.ConvertDownCallback), null);
		}

	#region thread callback stuff
		public static void ConvertDownCallback(object threadContext)
		{
			try
			{
				do_ConvertDown(threadContext);
				startX = startY = genWidth = conversionType = -1;				
			}
			catch (Exception e)
			{
				startX = startY = genWidth = conversionType = -1;
			}
		}

		public static void do_ConvertDown(object threadContext)
		{
			dodo_ConvertDown();
		}
	#endregion	
	
		public static void dodo_ConvertDown()
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
					tileIce = mod.TileType("MireIce");
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
			centerX -= (genWidth / 2);
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
					if (WorldGen.InWorld(k, l, 1))// && Math.Abs(k - i) + Math.Abs(l - j) < 6)
					{
						int type = (int)Main.tile[k, l].type;
						int wall = (int)Main.tile[k, l].wall;
						bool tileChanged = false;
						if (wallGrass != 0 && WallID.Sets.Conversion.Grass[wall] && wall != wallGrass)
						{
							tileChanged = true;							
							Main.tile[k, l].wall = (ushort)wallGrass;
							//WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (wallStone != 0 && WallID.Sets.Conversion.Stone[wall] && wall != wallStone)
						{
							tileChanged = true;							
							Main.tile[k, l].wall = (ushort)wallStone;
							//WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (wallSandHard != 0 && WallID.Sets.Conversion.HardenedSand[wall] && wall != wallSandHard)
						{
							tileChanged = true;						
							Main.tile[k, l].wall = (ushort)wallSandHard;
							//WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (wallSandstone != 0 && WallID.Sets.Conversion.Sandstone[wall] && wall != wallSandstone)
						{
							tileChanged = true;							
							Main.tile[k, l].wall = (ushort)wallSandstone;
							//WorldGen.SquareWallFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						if (tileStone != 0 && (Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != tileStone)
						{
							tileChanged = true;						
							Main.tile[k, l].type = (ushort)tileStone;
							//WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileGrass != 0 && TileID.Sets.Conversion.Grass[type] && type != tileGrass)
						{
							tileChanged = true;							
							Main.tile[k, l].type = (ushort)tileGrass;
							//WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileIce != 0 && TileID.Sets.Conversion.Ice[type] && type != tileIce)
						{
							tileChanged = true;							
							Main.tile[k, l].type = (ushort)tileIce;
							//WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileSand != 0 && TileID.Sets.Conversion.Sand[type] && type != tileSand)
						{
							tileChanged = true;							
							Main.tile[k, l].type = (ushort)tileSand;
							//WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileSandHard != 0 && TileID.Sets.Conversion.HardenedSand[type] && type != tileSandHard)
						{
							tileChanged = true;						
							Main.tile[k, l].type = (ushort)tileSandHard;
							//WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileSandstone != 0 && TileID.Sets.Conversion.Sandstone[type] && type != tileSandstone)
						{
							tileChanged = true;							
							Main.tile[k, l].type = (ushort)tileSandstone;
							//WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						else if (tileThorn != 0 && TileID.Sets.Conversion.Thorn[type] && type != tileThorn)
						{
							tileChanged = true;
							Main.tile[k, l].type = (ushort)tileThorn;
							//WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
						}
						if(FULLBRIGHT_MAP && tileChanged)
						{
							Main.Map.UpdateLighting(k, l, (byte)255);						
						}
					}
				}
			}
			if(FULLBRIGHT_MAP)
			{
				//Main.mapMinX = i - size; Main.mapMinY = i + size;
				//Main.mapMaxX = j - size; Main.mapMaxY = j + size;
				Main.mapMinX = 10; Main.mapMinY = 10;
				Main.mapMaxX = Main.maxTilesX - 10; Main.mapMaxY = Main.maxTilesY - 10;				
				Main.refreshMap = true;
				//Main.instance.DrawToMap();	
			}
		}
	}
}