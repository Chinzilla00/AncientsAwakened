using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using BaseMod;

namespace AAMod.Worldgeneration
{ 
	public class MireBiome : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Mod mod = AAMod.instance;
			bool DEV = true;			
			//--- Initial variable creation
			ushort tileGrass = (ushort)mod.TileType("MireGrass"), tileDirt = TileID.Mud, tileStone = (ushort)mod.TileType("Depthstone"),
			tileIce = (ushort)mod.TileType("Depthice"), tileSand = (ushort)mod.TileType("Depthsand"), tileSandHardened = (ushort)mod.TileType("DepthsandHardened"), tileSandstone = (ushort)mod.TileType("Depthsandstone");

			int worldSize = GetWorldSize();
			int biomeRadius = (worldSize == 3 ? 180 : worldSize == 2 ? 150 : 120), biomeRadiusHalf = biomeRadius / 2; //how deep the biome is (scaled by world size)	
			
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = mod.TileType("Depthstone");
            colorToTile[new Color(255, 128, 0)] = TileID.Mud;
            colorToTile[new Color(0, 255, 0)] = mod.TileType("MireGrass");
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 0, 255)] = mod.WallType("DepthstoneWall");
            colorToWall[Color.Black] = -1; //don't touch when genning
			
			TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Lake"), colorToTile, mod.GetTexture("Worldgeneration/LakeWalls"), colorToWall, mod.GetTexture("Worldgeneration/LakeWater"));
			Point newOrigin = new Point(origin.X, origin.Y - 10); //biomeRadius);

			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.JungleGrass, TileID.CorruptGrass, TileID.FleshGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new BaseMod.SetModTile(tileGrass, true, true) //actually place the tile
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Dirt }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileDirt, true, true)
			}));
			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //stone...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileStone, true, true)
			}));			
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //ice...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.IceBlock, TileID.CorruptIce, TileID.FleshIce }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileIce, true, true)
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //sand...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Sand, TileID.Ebonsand, TileID.Crimsand }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileSand, true, true)
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //hardened sand...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.HardenedSand, TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileSandHardened, true, true)
			}));
			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Sandstone, TileID.CorruptSandstone, TileID.CrimsonSandstone }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileSandstone, true, true)
			}));
			gen.Generate(origin.X - (gen.width / 2), origin.Y - 20, true, true);

			return true;
		}

        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6300) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }		
	}

    public class InfernoBiome : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance; //replace with your own
            bool DEV = true;
            //--- Initial variable creation
            ushort tileGrass = (ushort)mod.TileType("InfernoGrass"), tileStone = (ushort)mod.TileType("Torchstone"),
            tileIce = (ushort)mod.TileType("Torchice"), tileSand = (ushort)mod.TileType("Torchsand"), tileSandHardened = (ushort)mod.TileType("TorchsandHardened"), tileSandstone = (ushort)mod.TileType("Torchsandstone");

            int worldSize = GetWorldSize();
            int biomeRadius = (worldSize == 3 ? 240 : worldSize == 2 ? 180 : 150), biomeRadiusHalf = biomeRadius / 2; //how deep the biome is (scaled by world size)	

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = mod.TileType("Torchstone");
            colorToTile[new Color(0, 0, 255)] = mod.TileType("Torchstone");
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = mod.WallType("TorchstoneWall");
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Volcano"), colorToTile, mod.GetTexture("Worldgeneration/VolcanoWalls"), colorToWall, mod.GetTexture("Worldgeneration/VolcanoLava"));
            Point newOrigin = new Point(origin.X, origin.Y - 30); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
			{
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.CorruptGrass, TileID.FleshGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new BaseMod.SetModTile(tileGrass, true, true) //actually place the tile
			}));
            /* WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
             {
                 new Modifiers.OnlyTiles(new ushort[]{ TileID.Dirt }),
                 new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                 new BaseMod.SetModTile(tileDirt, true, true)
             }));*/
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //and stone.
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new BaseMod.SetModTile(tileStone, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //ice...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.IceBlock, TileID.CorruptIce, TileID.FleshIce }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new BaseMod.SetModTile(tileIce, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //sand...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sand, TileID.Ebonsand, TileID.Crimsand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new BaseMod.SetModTile(tileSand, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //hardened sand...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.HardenedSand, TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new BaseMod.SetModTile(tileSandHardened, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sandstone, TileID.CorruptSandstone, TileID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new BaseMod.SetModTile(tileSandstone, true, true)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y - 20, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6300) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class TerrariumDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning				


            Texture2D TerraSmall = mod.GetTexture("Worldgeneration/TerrariumDelete");
            Texture2D TerraMed = mod.GetTexture("Worldgeneration/TerrariumMedDelete");
            Texture2D TerraLarge = mod.GetTexture("Worldgeneration/TerrariumLargeDelete");

            TexGen gen = BaseWorldGenTex.GetTexGenerator(worldSize == 3 ? TerraLarge : worldSize == 2 ? TerraMed : TerraSmall, colorToTile);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6300) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class TerrariumSphere : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = mod.TileType("TerraCrystal");
            colorToTile[new Color(255, 0, 255)] = mod.TileType("TerraWood");
            colorToTile[new Color(255, 255, 0)] = mod.TileType("TerraLeaves");
            colorToTile[new Color(0, 0, 255)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 255, 0)] = -2;
            colorToWall[Color.Black] = -1; //don't touch when genning				
            

            Texture2D TerraSmall = mod.GetTexture("Worldgeneration/Terrarium");
            Texture2D TerraMed = mod.GetTexture("Worldgeneration/TerrariumMed");
            Texture2D TerraLarge = mod.GetTexture("Worldgeneration/TerrariumLarge");

            Texture2D WallSmall = mod.GetTexture("Worldgeneration/TerrariumWalls");
            Texture2D WallMed = mod.GetTexture("Worldgeneration/TerrariumMedWalls");
            Texture2D WallLarge = mod.GetTexture("Worldgeneration/TerrariumLargeWalls");

            TexGen gen = BaseWorldGenTex.GetTexGenerator(worldSize == 3 ? TerraLarge : worldSize == 2 ? TerraMed : TerraSmall, colorToTile, worldSize == 3 ? WallLarge : worldSize == 2 ? WallMed : WallSmall, colorToWall);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6300) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class Parthenan : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = mod.TileType("FulguritePlatingS");
            colorToTile[new Color(255, 0, 0)] = mod.TileType("FulguriteBrickS");
            colorToTile[new Color(0, 0, 255)] = mod.TileType("StormCloud");
            colorToTile[new Color(255, 0, 255)] = mod.TileType("FulgurGlassS");
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(0, 255, 0)] = mod.TileType("FulguritePlatingWallS");
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Parthenan"), colorToTile, mod.GetTexture("Worldgeneration/ParthenanWalls"), colorToWall);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);
            
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
    }

    public class RadialDitherTopMiddle2 : GenAction
	{
		private int _width, _height;
		private float _innerRadius, _outerRadius;

		public RadialDitherTopMiddle2(int width, int height, float innerRadius, float outerRadius)
		{
			_width = width;
			_height = height;
			_innerRadius = innerRadius;
			_outerRadius = outerRadius;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			Vector2 value = new Vector2((float)origin.X + (_width / 2), (float)origin.Y);
			Vector2 value2 = new Vector2((float)x, (float)y);
			float num = Vector2.Distance(value2, value);
			float num2 = Math.Max(0f, Math.Min(1f, (num - this._innerRadius) / (this._outerRadius - this._innerRadius)));
			if (_random.NextDouble() > (double)num2)
			{
				return base.UnitApply(origin, x, y, args);
			}
			return base.Fail();
		}
	}	
}