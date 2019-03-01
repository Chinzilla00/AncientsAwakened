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
using AAMod.Tiles;

namespace AAMod.Worldgeneration
{ 
	public class MireBiome : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Mod mod = AAMod.instance;
			ushort tileGrass = (ushort)mod.TileType("MireGrass"), tileDirt = TileID.Mud, tileStone = (ushort)mod.TileType("Depthstone"),
			tileIce = (ushort)mod.TileType("Depthice"), tileSand = (ushort)mod.TileType("Depthsand"), tileSandHardened = (ushort)mod.TileType("DepthsandHardened"), tileSandstone = (ushort)mod.TileType("Depthsandstone");

			int worldSize = GetWorldSize();
			int biomeRadius = (worldSize == 3 ? 220 : worldSize == 2 ? 180 : 150), biomeRadiusHalf = biomeRadius / 2; //how deep the biome is (scaled by world size)	
			
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = mod.TileType("Depthstone");
            colorToTile[new Color(255, 128, 0)] = mod.TileType("Darkmud");
            colorToTile[new Color(0, 255, 0)] = mod.TileType("AbyssGrass");
            colorToTile[new Color(255, 0, 0)] = mod.TileType("AbyssWood");
            colorToTile[new Color(128, 0, 0)] = mod.TileType("AbyssWoodSolid");
            colorToTile[new Color(255, 255, 0)] = mod.TileType("AbyssVines");
            colorToTile[new Color(255, 0, 255)] = mod.TileType("AbyssLeaves");
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
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone, TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone }),
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
			int genX = origin.X - (gen.width / 2);
			int genY = origin.Y;
			gen.Generate(genX, genY, true, true);


            WorldGen.PlaceObject(genX + 25, genY + 204, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 44, genY + 212, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 50, genY + 222, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 82, genY + 224, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 104, genY + 232, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 125, genY + 223, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 144, genY + 217, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 162, genY + 215, mod.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 172, genY + 206, mod.TileType<HydraPod>());

            WorldGen.PlaceObject(genX + 60, genY + 31, mod.TileType<DreadAltarS>());		   

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        Tile tile = Main.tile[AltarX, AltarY];
                        if (Main.rand.Next(15) == 0)
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, mod.TileType<ChaosAltar1>());
                        }
                    }
                }
            }
            return true;
		}
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }		
	}

    public class MireDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(0, 0, 255)] = -2;
            colorToTile[new Color(255, 128, 0)] = -2;
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[new Color(255, 0, 0)] = -2;
            colorToTile[new Color(128, 0, 0)] = -2;
            colorToTile[new Color(255, 255, 0)] = -2;
            colorToTile[new Color(255, 0, 255)] = -2;
            colorToTile[Color.Black] = -1;

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Lake"), colorToTile);
			int genX = origin.X - (gen.width / 2);
			int genY = origin.Y;			
            gen.Generate(genX, genY, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class InfernoBiome : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            //--- Initial variable creation
            ushort tileGrass = (ushort)mod.TileType("InfernoGrass"), tileStone = (ushort)mod.TileType("Torchstone"),
            tileIce = (ushort)mod.TileType("Torchice"), tileSand = (ushort)mod.TileType("Torchsand"), tileSandHardened = (ushort)mod.TileType("TorchsandHardened"), tileSandstone = (ushort)mod.TileType("Torchsandstone");

            int worldSize = GetWorldSize();
            int biomeRadius = (worldSize == 3 ? 240 : worldSize == 2 ? 180 : 150), biomeRadiusHalf = biomeRadius / 2; //how deep the biome is (scaled by world size)	

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = mod.TileType("Torchstone");
            colorToTile[new Color(0, 0, 255)] = mod.TileType("Torchstone");
            colorToTile[new Color(0, 255, 0)] = mod.TileType("ScorchedDynastyWoodS");
            colorToTile[new Color(255, 255, 0)] = mod.TileType("ScorchedShinglesS");
            colorToTile[new Color(255, 0, 255)] = mod.TileType("ScorchedPlatform");
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = mod.WallType("TorchstoneWall");
            colorToWall[new Color(0, 0, 255)] = mod.WallType("BurnedDynastyWall");
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
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone, TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone }),
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
			int genX = origin.X - (gen.width / 2);
			int genY = origin.Y - 80;
            gen.Generate(genX, genY, true, true);

            WorldGen.PlaceObject(genX + 65, genY + 19, mod.TileType<DracoAltarS>());
            WorldGen.PlaceObject(genX + 34, genY + 314, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 47, genY + 315, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 58, genY + 317, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 68, genY + 317, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 79, genY + 318, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 88, genY + 316, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 97, genY + 313, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 104, genY + 308, mod.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 25, genY + 308, mod.TileType<DragonEgg>());

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        Tile tile = Main.tile[AltarX, AltarY];
                        if (Main.rand.Next(15) == 0)
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, mod.TileType<ChaosAltar2>());
                        }
                    }
                }
            }

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class InfernoDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = -2;
            colorToTile[new Color(0, 0, 255)] = -2;
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[new Color(255, 255, 0)] = -2;
            colorToTile[new Color(255, 0, 255)] = -2;
            colorToTile[new Color(150, 150, 150)] = -2;
            colorToTile[Color.Black] = -1;

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Volcano"), colorToTile);
            Point newOrigin = new Point(origin.X, origin.Y - 30);
            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 80;		
            gen.Generate(genX, genY, true, true);						

            return true;
        }
    }

    public class SurfaceMushroom : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            //--- Initial variable creation
            ushort tileGrass = (ushort)mod.TileType("Mycelium");

            int worldSize = GetWorldSize();
            int biomeRadius = (worldSize == 3 ? 100 : 80), biomeRadiusHalf = biomeRadius / 2;	
			

            Point newOrigin = new Point(origin.X, origin.Y - 30);

            Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), new Actions.TileScanner(new ushort[]
            {
                TileID.Grass,
                TileID.Dirt,
                TileID.Stone,
                TileID.Sand,
                TileID.SnowBlock,
                TileID.IceBlock,
                TileID.BlueDungeonBrick,
                TileID.PinkDungeonBrick,
                TileID.GreenDungeonBrick,
                (ushort)mod.TileType<Torchstone>(),
                (ushort)mod.TileType<Torchsand>(),
                (ushort)mod.TileType<Torchsandstone>(),
                (ushort)mod.TileType<TorchsandHardened>(),
                (ushort)mod.TileType<Torchice>(),
                (ushort)mod.TileType<InfernoGrass>(),
                (ushort)mod.TileType<Depthstone>(),
                (ushort)mod.TileType<Depthsand>(),
                (ushort)mod.TileType<Depthsandstone>(),
                (ushort)mod.TileType<DepthsandHardened>(),
                (ushort)mod.TileType<Depthice>(),
                (ushort)mod.TileType<MireGrass>(),
            }).Output(dictionary));

            int normalBiomeCount = dictionary[TileID.Grass] + dictionary[TileID.Dirt] + dictionary[TileID.Stone];
            int IceBlockBiomeCount = dictionary[TileID.SnowBlock] + dictionary[TileID.IceBlock];
            int sandBiomeCount = dictionary[TileID.Sand];
            int dungeonCount = dictionary[TileID.BlueDungeonBrick] + dictionary[TileID.PinkDungeonBrick] + dictionary[TileID.GreenDungeonBrick];
            int InfernoBiomeCount = dictionary[(ushort)mod.TileType<InfernoGrass>()] + dictionary[(ushort)mod.TileType<Torchstone>()] + dictionary[(ushort)mod.TileType<Torchsandstone>()] + dictionary[(ushort)mod.TileType<TorchsandHardened>()] + dictionary[(ushort)mod.TileType<Torchice>()];
            int MireBiomeCount = dictionary[(ushort)mod.TileType<MireGrass>()] + dictionary[(ushort)mod.TileType<Depthstone>()] + dictionary[(ushort)mod.TileType<Depthsandstone>()] + dictionary[(ushort)mod.TileType<DepthsandHardened>()] + dictionary[(ushort)mod.TileType<Depthice>()];


            if (dungeonCount > 0 || IceBlockBiomeCount >= normalBiomeCount || MireBiomeCount >= normalBiomeCount || InfernoBiomeCount >= normalBiomeCount || sandBiomeCount >= normalBiomeCount) //don't gen if you're in the Dungeon at all or if the Ice count (Snow) or the Sand count (desert) is too high
            {
                return false;
            }
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.CorruptGrass, TileID.FleshGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new BaseMod.SetModTile(tileGrass, true, true) //actually place the tile
			}));

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX == 4200) { return 1; }
            else if (Main.maxTilesX == 6400) { return 2; }
            else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }

    public class TerrariumDelete : MicroBiome
    {

        Texture2D Terrasphere = null;

        Texture2D TerraWalls = null;

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

            if (Terrasphere == null)
            {
                if (worldSize == 3)
                {
                    Terrasphere = TerraLarge;
                }
                if (worldSize == 2)
                {
                    Terrasphere = TerraMed;
                }
                if (worldSize == 1)
                {
                    Terrasphere = TerraSmall;
                }
            }

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
            //if (Main.maxTilesX == 4200) { return 1; }
            //else if (Main.maxTilesX == 6400) { return 2; }
            //else if (Main.maxTilesX == 8400) { return 3; }
            return 1; //unknown size, assume small
        }
    }
    
    public class TerrariumSphere : MicroBiome
    {
        Texture2D Terrasphere = null;

        Texture2D TerraWalls = null;

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

            if (Terrasphere == null)
            {
                if (worldSize == 3)
                {
                    Terrasphere = TerraLarge;

                    TerraWalls = WallLarge;
                }
                if (worldSize == 2)
                {
                    Terrasphere = TerraMed;

                    TerraWalls = WallMed;
                }
                if (worldSize == 1)
                {
                    Terrasphere = TerraSmall;

                    TerraWalls = WallSmall;
                }
            }

            TexGen gen = BaseWorldGenTex.GetTexGenerator(Terrasphere, colorToTile, TerraWalls, colorToWall);
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
            //if (Main.maxTilesX == 4200) { return 1; }
            //else if (Main.maxTilesX == 6400) { return 2; }
            //else if (Main.maxTilesX == 8400) { return 3; }
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
            colorToWall[new Color(0, 255, 0)] = mod.WallType("FulguritePlatingWallS");
            colorToWall[new Color(255, 0, 255)] = mod.TileType("FulgurGlassWall");
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Parthenan"), colorToTile, mod.GetTexture("Worldgeneration/ParthenanWalls"), colorToWall);
            
            gen.Generate(origin.X, origin.Y, true, true);
            WorldGen.PlaceObject((int)(origin.X) + 34, (int)(origin.Y) + 47, (ushort)mod.TileType("DataBank"));
            WorldGen.PlaceChest((origin.X) + 32, (origin.Y) + 47, (ushort)mod.TileType("StormChest"), true);
            WorldGen.PlaceChest((origin.X) + 41, (origin.Y) + 47, (ushort)mod.TileType("StormChest"), true);
            return true;
        }
    }

    public class BOTE : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = mod.TileType("RottedDynastyWoodS");
            colorToTile[new Color(0, 255, 0)] = mod.TileType("RottedPlatform");
            colorToTile[new Color(0, 0, 255)] = TileID.Rope;
            colorToTile[new Color(0, 255, 255)] = mod.TileType("CthulhuPortal");
            colorToTile[new Color(255, 255, 0)] = TileID.Sand;			
            colorToTile[new Color(150, 150, 150)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = mod.WallType("RottedFence");
            colorToWall[new Color(255, 255, 0)] = mod.WallType("RottedWall");
            colorToWall[new Color(255, 255, 255)] = mod.WallType("RottedWall");
            colorToWall[new Color(0, 255, 255)] = mod.WallType("RottedWall");
            colorToWall[new Color(255, 0, 255)] = mod.WallType("RottedWall");
            colorToWall[new Color(0, 255, 0)] = mod.WallType("RottedWall");
            colorToWall[new Color(0, 0, 255)] = WallID.Sail;
            colorToWall[new Color(150, 150, 150)] = -2;
            colorToWall[Color.Black] = -1; //don't touch when genning				

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/BOTE"), colorToTile, mod.GetTexture("Worldgeneration/BOTEWalls"), colorToWall, mod.GetTexture("Worldgeneration/BOTEWater"));

			int newOriginX = origin.X - (gen.width / 2);
			int newOriginY = origin.Y - (gen.height / 2) + 10;
            gen.Generate(newOriginX, newOriginY, true, true);
            
            WorldGen.PlaceChest(newOriginX + 130, newOriginY + 102, (ushort)mod.TileType("SunkenChest"), true);
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