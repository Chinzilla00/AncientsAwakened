using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

using AAMod.Tiles;
using AAMod.Walls;
using Terraria.Utilities;
using AAMod.Tiles.Chests;
using AAMod.Tiles.Crafters;
using AAMod.Tiles.Boss;
using AAMod.Tiles.Furniture.Razewood;
using AAMod.Tiles.Furniture.Terra;
using AAMod.Tiles.Furniture.Keep;
using AAMod.Tiles.Keep;
using AAMod.Tiles.Decoration;
using AAMod.Worldgeneration.Placeholder;

namespace AAMod.Worldgeneration
{
    public class MireBiome : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Mod mod = AAMod.instance;
            ushort tileGrass = (ushort)mod.TileType("MireGrass"), tileDirt = TileID.Mud, tileStone = (ushort)mod.TileType("Depthstone"), tileIce = (ushort)mod.TileType("IndigoIce"),
            tileSand = (ushort)mod.TileType("Depthsand"), tileSandHardened = (ushort)mod.TileType("DepthsandHardened"), tileSandstone = (ushort)mod.TileType("Depthsandstone"),
            LivingWood = (ushort)ModContent.TileType<LivingBogwood>(), LivingLeaves = (ushort)ModContent.TileType<LivingBogleaves>();

            int StoneWall = ModContent.WallType<DepthstoneWall>(), SandstoneWall = ModContent.WallType<DepthsandstoneWall>(), HardenedSandWall = ModContent.WallType<DepthsandHardenedWall>(),
            GrassWall = ModContent.WallType<LivingBogleafWall>(), JungleWall = ModContent.WallType<MireJungleWall>();

			int worldSize = GetWorldSize();
			int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180; //how deep the biome is (scaled by world size)	

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = mod.TileType("Depthstone"),
                [new Color(255, 128, 0)] = mod.TileType("Darkmud"),
                [new Color(0, 255, 0)] = mod.TileType("AbyssGrass"),
                [new Color(255, 0, 0)] = mod.TileType("AbyssWood"),
                [new Color(128, 0, 0)] = mod.TileType("AbyssWoodSolid"),
                [new Color(255, 255, 0)] = mod.TileType("AbyssVines"),
                [new Color(0, 255, 255)] = mod.TileType("DepthMoss"),
                [new Color(255, 0, 255)] = mod.TileType("AbyssLeaves"),
                [new Color(128, 0, 0)] = mod.TileType("AbyssWoodSolid"),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = mod.WallType("DepthstoneWall"),
                [Color.Black] = -1 //don't touch when genning
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Lake"), colorToTile, mod.GetTexture("Worldgeneration/LakeWalls"), colorToWall, mod.GetTexture("Worldgeneration/LakeWater"));
			Point newOrigin = new Point(origin.X, origin.Y - 10); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
				new InWorld(),				
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.JungleGrass, TileID.CorruptGrass, TileID.FleshGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new SetModTile(tileGrass, true, true) //actually place the tile
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
			{
				new InWorld(),				
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Dirt }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new SetModTile(tileDirt, true, true)
			}));
			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //stone...
			{
				new InWorld(),				
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone, TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new SetModTile(tileStone, true, true)
			}));			
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //ice...
			{
				new InWorld(),				
				new Modifiers.OnlyTiles(new ushort[]{ TileID.IceBlock, TileID.CorruptIce, TileID.FleshIce }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new SetModTile(tileIce, true, true)
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //sand...
			{
				new InWorld(),				
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Sand, TileID.Ebonsand, TileID.Crimsand }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new SetModTile(tileSand, true, true)
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //hardened sand...
			{
				new InWorld(),				
				new Modifiers.OnlyTiles(new ushort[]{ TileID.HardenedSand, TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new SetModTile(tileSandHardened, true, true)
			}));
			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
				new InWorld(),				
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Sandstone, TileID.CorruptSandstone, TileID.CrimsonSandstone }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new SetModTile(tileSandstone, true, true)
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and Living Wood.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahogany, TileID.LivingWood}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingWood, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and Living Leaves.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahoganyLeaves, TileID.LeafBlock}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingLeaves, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new byte[]{ WallID.Stone, WallID.EbonstoneUnsafe, WallID.CrimstoneUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(StoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.Sandstone, WallID.CorruptSandstone, WallID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(SandstoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),
				new Modifiers.OnlyWalls(new byte[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.GrassUnsafe, WallID.CorruptGrassUnsafe, WallID.CrimsonGrassUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(GrassWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.JungleUnsafe, WallID.JungleUnsafe1, WallID.JungleUnsafe2, WallID.JungleUnsafe3, WallID.JungleUnsafe4 }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(JungleWall, true)
            }));

            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 30;
            gen.Generate(genX, genY, true, true);


            WorldGen.PlaceObject(genX + 24, genY + 203, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 43, genY + 211, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 59, genY + 221, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 81, genY + 223, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 103, genY + 231, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 124, genY + 222, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 143, genY + 216, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 161, genY + 214, ModContent.TileType<HydraPod>());
            WorldGen.PlaceObject(genX + 171, genY + 205, ModContent.TileType<HydraPod>());
            NetMessage.SendObjectPlacment(-1, genX + 25, genY + 204, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 43, genY + 211, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 59, genY + 221, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 81, genY + 223, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 103, genY + 231, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 124, genY + 222, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 143, genY + 216, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 161, genY + 214, mod.TileType("HydraPod"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 171, genY + 205, mod.TileType("HydraPod"), 0, 0, -1, -1);

            //WorldGen.PlaceObject(genX + 59, genY + 31, Terraria.ModLoader.ModContent.TileType<DreadAltarS>());		   

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        if (Main.rand.Next(15) == 0)
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, ModContent.TileType<ChaosAltar1>());
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

    public class BogwoodCon : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;
            ushort LivingWood = (ushort)ModContent.TileType<LivingBogwood>(), LivingLeaves = (ushort)ModContent.TileType<LivingBogleaves>();

            byte BogwoodWall = (byte)ModContent.WallType<LivingBogwoodWall>(), LeafWall = (byte)ModContent.WallType<LivingBogleafWall>();

            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;
            Point newOrigin = new Point(origin.X, origin.Y - 10);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Living Wood.
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahogany, TileID.LivingWood}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingWood, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and Living Leaves.
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahoganyLeaves, TileID.LeafBlock}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingLeaves, true, true)
            }));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[]
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.LivingWood }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(BogwoodWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
                new InWorld(),
                new Modifiers.OnlyWalls(new byte[]{ WallID.LivingLeaf }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(LeafWall, true)
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

    public class MireDelete : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = -2,
                [new Color(255, 128, 0)] = -2,
                [new Color(0, 255, 0)] = -2,
                [new Color(255, 0, 0)] = -2,
                [new Color(128, 0, 0)] = -2,
                [new Color(255, 255, 0)] = -2,
                [new Color(255, 0, 255)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Lake"), colorToTile);
			int genX = origin.X - (gen.width / 2);
			int genY = origin.Y - 30;			
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
            ushort tileGrass = (ushort)mod.TileType("InfernoGrass"), tileStone = (ushort)mod.TileType("Torchstone"), tileSnow = (ushort)mod.TileType("TorchAsh"),
            tileIce = (ushort)mod.TileType("Torchice"), tileSand = (ushort)mod.TileType("Torchsand"), tileSandHardened = (ushort)mod.TileType("TorchsandHardened"), tileSandstone = (ushort)mod.TileType("Torchsandstone"),
            LivingWood = (ushort)ModContent.TileType<LivingRazewood>(), LivingLeaves = (ushort)ModContent.TileType<LivingRazeleaves>();

            int StoneWall = ModContent.WallType<TorchstoneWall>(), SandstoneWall = ModContent.WallType<TorchsandstoneWall>(), 
                HardenedSandWall = ModContent.WallType<TorchsandHardenedWall>(), GrassWall = ModContent.WallType<InfernoGrassWall>();


            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 240 : worldSize == 2 ? 200 : 180;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.TileType("Torchstone"),
                [new Color(0, 0, 255)] = mod.TileType("Torchstone"),
                [new Color(0, 255, 0)] = mod.TileType("ScorchedDynastyWoodS"),
                [new Color(255, 255, 0)] = mod.TileType("ScorchedShinglesS"),
                [new Color(255, 0, 255)] = mod.TileType("ScorchedPlatform"),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.WallType("TorchstoneWall"),
                [new Color(0, 0, 255)] = mod.WallType("BurnedDynastyWall"),
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Volcano"), colorToTile, mod.GetTexture("Worldgeneration/VolcanoWalls"), colorToWall, mod.GetTexture("Worldgeneration/VolcanoLava"));
            Point newOrigin = new Point(origin.X, origin.Y - 30); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
				new InWorld(),				
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),			
                new Actions.SetLiquid(1, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
			{
				new InWorld(),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.CorruptGrass, TileID.FleshGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new SetModTile(tileGrass, true, true) //actually place the tile
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
            {
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[] { TileID.SnowBlock }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSnow, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //and stone.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone, TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileStone, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //ice...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.IceBlock, TileID.CorruptIce, TileID.FleshIce }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileIce, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //sand...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sand, TileID.Ebonsand, TileID.Crimsand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSand, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //hardened sand...
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.HardenedSand, TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSandHardened, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Sandstone, TileID.CorruptSandstone, TileID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(tileSandstone, true, true)
            }));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and sandstone.
			{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LeafBlock }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingLeaves, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //...and Living Wood.
			{
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.LivingMahogany, TileID.LivingWood}),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new SetModTile(LivingWood, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new byte[]{ WallID.Stone, WallID.EbonstoneUnsafe, WallID.CrimstoneUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(StoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new byte[]{ WallID.Sandstone, WallID.CorruptSandstone, WallID.CrimsonSandstone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(SandstoneWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new byte[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new byte[]{ WallID.HardenedSand, WallID.CorruptHardenedSand, WallID.CrimsonHardenedSand }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(HardenedSandWall, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //Walls
			{
				new InWorld(),				
                new Modifiers.OnlyWalls(new byte[]{ WallID.GrassUnsafe, WallID.CorruptGrassUnsafe, WallID.CrimsonGrassUnsafe }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new PlaceModWall(GrassWall, true)
            }));

            int genX = origin.X - (gen.width / 2);
            int genY = origin.Y - 80;
            gen.Generate(genX, genY, true, true);

            //WorldGen.PlaceObject(genX + 65, genY + 4, Terraria.ModLoader.ModContent.TileType<DracoAltarS>());
            WorldGen.PlaceObject(genX + 24, genY + 307, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 33, genY + 313, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 46, genY + 314, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 57, genY + 316, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 67, genY + 316, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 78, genY + 317, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 87, genY + 315, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 96, genY + 312, ModContent.TileType<DragonEgg>());
            WorldGen.PlaceObject(genX + 103, genY + 307, ModContent.TileType<DragonEgg>());
            NetMessage.SendObjectPlacment(-1, genX + 24, genY + 307, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 33, genY + 313, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 46, genY + 314, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 57, genY + 316, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 67, genY + 316, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 78, genY + 317, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 87, genY + 315, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 96, genY + 312, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);
            NetMessage.SendObjectPlacment(-1, genX + 103, genY + 307, (ushort)mod.TileType("DragonEgg"), 0, 0, -1, -1);

            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = origin.X + WorldGen.genRand.Next(0, biomeRadius);
                int yAxis = origin.Y + WorldGen.genRand.Next(0, biomeRadius);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        if (Main.rand.Next(15) == 0)
                        {
                            WorldGen.PlaceObject(AltarX, AltarY - 1, ModContent.TileType<ChaosAltar2>());
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

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = -2,
                [new Color(0, 0, 255)] = -2,
                [new Color(0, 255, 0)] = -2,
                [new Color(255, 255, 0)] = -2,
                [new Color(255, 0, 255)] = -2,
                [new Color(150, 150, 150)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Volcano"), colorToTile);
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
            Mod mod = AAMod.instance;

            ushort tileGrass = (ushort)mod.TileType("Mycelium"); //change to types in your mod

            int worldSize = GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150, biomeWidthHalf = biomeWidth / 2; //how wide the biome is (scaled by world size)
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;

            //ok time to check to see if this spot is actually a good place to gen
            Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
            Point newOrigin = new Point(origin.X - biomeWidthHalf, origin.Y - 10);
            WorldUtils.Gen(newOrigin, new Shapes.Rectangle(biomeWidth, biomeHeight), new Actions.TileScanner(new ushort[]
            {
                TileID.Grass,
                TileID.Dirt,
                TileID.Stone,
                TileID.Sand,
                TileID.SnowBlock,
                TileID.IceBlock,
                TileID.BlueDungeonBrick,
                TileID.PinkDungeonBrick,
                TileID.GreenDungeonBrick
            }).Output(dictionary));

            int normalBiomeCount = dictionary[TileID.Grass] + dictionary[TileID.Dirt] + dictionary[TileID.Stone];
            int IceBlockBiomeCount = dictionary[TileID.SnowBlock] + dictionary[TileID.IceBlock];
            int sandBiomeCount = dictionary[TileID.Sand];
            int dungeonCount = dictionary[TileID.BlueDungeonBrick] + dictionary[TileID.PinkDungeonBrick] + dictionary[TileID.GreenDungeonBrick];

            if (dungeonCount > 0 || IceBlockBiomeCount >= normalBiomeCount || sandBiomeCount >= normalBiomeCount) //don't gen if you're in the Dungeon at all or if the Ice count (Snow) or the Sand count (desert) is too high
            {
                return false;
            }
            WorldUtils.Gen(newOrigin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[] //gen grass...
            {
				new InWorld(),				
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass }), //ensure we only replace the intended tile (in this case, grass)
                new RadialDitherTopMiddle(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10), //this provides the 'blending' on the edges (except the top)
                new SetModTile(tileGrass, true, true) //actually place the tile
            }));
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
        Texture2D Terrasphere = null;
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;
            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 400 : worldSize == 2 ? 300 : 200;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = -2,
                [Color.Black] = -1 //don't touch when genning		
            };


            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToTile[new Color(0, 255, 0)] = -2;
            colorToTile[Color.Black] = -1; //don't touch when genning	


            Texture2D TerraSmall = mod.GetTexture("Worldgeneration/TerrariumDelete");
            Texture2D TerraMed = mod.GetTexture("Worldgeneration/TerrariumMedDelete");

            if (Terrasphere == null)
            {
                if (worldSize == 1)
                {
                    Terrasphere = TerraSmall;
                }
                else
                {
                    Terrasphere = TerraMed;
                }
            }

            TexGen gen = BaseWorldGenTex.GetTexGenerator(Terrasphere, colorToTile, Terrasphere, colorToWall);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
				new InWorld(),				
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
				new InWorld(),				
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

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

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = mod.TileType("TerraCrystal"),
                [new Color(255, 0, 255)] = mod.TileType("TerraWood"),
                [new Color(255, 255, 0)] = mod.TileType("TerraLeaves"),
                [new Color(0, 0, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = -2,
                [Color.Black] = -1 //don't touch when genning				
            };


            Texture2D TerraSmall = mod.GetTexture("Worldgeneration/Terrarium");
            Texture2D TerraMed = mod.GetTexture("Worldgeneration/TerrariumMed");

            Texture2D WallSmall = mod.GetTexture("Worldgeneration/TerrariumWalls");
            Texture2D WallMed = mod.GetTexture("Worldgeneration/TerrariumMedWalls");

            if (Terrasphere == null)
            {
                if (worldSize == 1)
                {
                    Terrasphere = TerraSmall;

                    TerraWalls = WallSmall;
                }
                else
                {
                    Terrasphere = TerraMed;

                    TerraWalls = WallMed;
                }
            }

            TexGen gen = BaseWorldGenTex.GetTexGenerator(Terrasphere, colorToTile, TerraWalls, colorToWall);
            Point newOrigin = new Point(origin.X, origin.Y); //biomeRadius);

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //remove all fluids in sphere...
            {
				new InWorld(),				
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new Actions.SetLiquid(0, 0)
            }));
            WorldUtils.Gen(new Point(origin.X - (gen.width / 2), origin.Y - 20), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[] //remove all fluids in the volcano...
            {
				new InWorld(),				
                new Actions.SetLiquid(0, 0)
            }));
            gen.Generate(origin.X - (gen.width / 2), origin.Y, true, true);

            return true;
        }
        public static int GetWorldSize()
        {
            if (Main.maxTilesX <= 4200) { return 1; }
            else { return 2; }
        }
    }

    public class HoardClear : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = -2,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/GreedNestClear"), colorToTile);
            gen.Generate(origin.X, origin.Y, true, true);

            return true;
        }
    }

    public class Hoard : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.TileType("GreedStone"),
                [new Color(0, 0, 255)] = mod.TileType("GreedBrick"),
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/GreedNest"), colorToTile, mod.GetTexture("Worldgeneration/GreedNestWalls"), colorToWall);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldUtils.Gen(new Point(origin.X, origin.Y), new Shapes.Rectangle(gen.width, gen.height), Actions.Chain(new GenAction[]
			{
                new InWorld(),
                new Actions.SetLiquid(0, 0)
            }));

            HoardChest(origin.X + 19, origin.Y + 55);
            HoardChest(origin.X + 38, origin.Y + 67, 1);
            HoardChest(origin.X + 25, origin.Y + 34);
            HoardChest(origin.X + 41, origin.Y + 27);
            HoardChest(origin.X + 53, origin.Y + 38);
            HoardChest(origin.X + 49, origin.Y + 54);
            HoardChest(origin.X + 67, origin.Y + 70, 2);
            HoardChest(origin.X + 79, origin.Y + 61);
            HoardChest(origin.X + 72, origin.Y + 41);
            HoardChest(origin.X + 95, origin.Y + 45);
            HoardChest(origin.X + 107, origin.Y + 57);
            HoardChest(origin.X + 121, origin.Y + 33);
            HoardChest(origin.X + 131, origin.Y + 48, 3);
            HoardChest(origin.X + 130, origin.Y + 69);

            WorldGen.PlaceObject(origin.X + 80, origin.Y + 88, mod.TileType("GreedAltar"));
            NetMessage.SendObjectPlacment(-1, origin.X + 80, origin.Y + 88, mod.TileType("GreedAltar"), 0, 0, -1, -1);

            return true;
        }

        public void HoardChest(int x, int y, int specialItem = 0)
        {
            int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<GreedChest>(), false, 1);

            int[] GreedChestLoot = new int[] {

                ItemID.GoldenChair,
                ItemID.GoldenToilet,
                ItemID.GoldenDoor,
                ItemID.GoldenTable,
                ItemID.GoldenBed,
                ItemID.GoldenPiano,
                ItemID.GoldenDresser,
                ItemID.GoldenSofa,
                ItemID.GoldenSink,
                ItemID.GoldenBathtub,
                ItemID.GoldenClock,
                ItemID.GoldenLamp,
                ItemID.GoldenBookcase,
                ItemID.GoldenChandelier,
                ItemID.GoldenLantern,
                ItemID.GoldenCandelabra,
                ItemID.GoldenCandle,
                ItemID.GoldenChest,
                ItemID.GoldenWorkbench,
                ItemID.GoldWatch,
                ItemID.GoldDust,
                ItemID.AncientGoldHelmet,
                ItemID.GoldBunny,
                ItemID.GoldButterfly,
                ItemID.GoldFrog,
                ItemID.GoldGrasshopper,
                ItemID.SquirrelGold,
                ItemID.GoldBird,
                ItemID.GoldMouse,
                ItemID.GoldWorm,
                ItemID.GoldCrown,
                ItemID.GoldenKey,
                ItemID.Goldfish,
                ItemID.ReflectiveGoldDye,
                ItemID.GoldGreaves,
                ItemID.GoldHelmet,
                ItemID.FindingGold,
                ItemID.GoldChainmail,
                ItemID.GoldShortsword,
                ItemID.GoldBroadsword,
                ItemID.GoldBow,
                ItemID.GoldHammer,
                ItemID.GoldPickaxe,
                ItemID.GoldenCrate
            };

            int[] Loot = new int[]
            {
                ItemID.CoinGun,
                ItemID.Cutlass,
                ItemID.DiscountCard,
                ItemID.GoldRing,
                ItemID.LuckyCoin,
            };

            int[] Loot2 = new int[]
            {
                ModContent.ItemType<Items.Armor.AncientGold.AncientGoldBody>(),
                ModContent.ItemType<Items.Armor.AncientGold.AncientGoldLeg>(),
            };

            if (PlacementSuccess >= 0)
            {
                Chest chest = Main.chest[PlacementSuccess];

                Item item0 = chest.item[0];
                UnifiedRandom genRand0 = WorldGen.genRand;
                int type;
                if (specialItem == 1)
                {
                    type = ModContent.ItemType<Items.Ranged.OdinsBlade>();
                }
                else if (specialItem == 2)
                {
                    type = ModContent.ItemType<Items.Melee.RomulusTazesaber>();
                }
                else if (specialItem == 3)
                {
                    type = ModContent.ItemType<Items.Misc.AnubisBook>();
                }
                else if (genRand0.Next(100) < 2f)
                {
                    type = Utils.Next(genRand0, Loot2);
                }
                else
                {
                    type = Utils.Next(genRand0, Loot);
                }

                item0.SetDefaults(type, false);

                chest.item[1].SetDefaults(ItemID.GoldBar);
                chest.item[1].stack = WorldGen.genRand.Next(70, 90);

                Item item = chest.item[2];
                item.SetDefaults(ItemID.FlaskofGold);
                chest.item[2].stack = WorldGen.genRand.Next(1, 4);

                chest.item[3].SetDefaults(ItemID.GoldCoin, false);
                chest.item[3].stack = WorldGen.genRand.Next(70, 90);

                for (int i = 0; i < 20; i++)
                {
                    chest.item[i + 4].SetDefaults(Utils.Next(WorldGen.genRand, GreedChestLoot));
                    if (chest.item[i + 4].maxStack > 1)
                    {
                        chest.item[i + 4].stack = WorldGen.genRand.Next(1, 3);
                    }
                }
            }

            NetMessage.SendObjectPlacment(-1, x, y, (ushort)ModContent.TileType<GreedChest>(), 1, 0, -1, -1);
        }
    }

    public class Acropolis : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.TileType("AcropolisBlock"),
                [new Color(128, 128, 128)] = mod.TileType("AcropolisBlock2"),
                [new Color(255, 255, 0)] = mod.TileType("SkyShard"),
                [new Color(0, 255, 255)] = TileID.Grass,
                [new Color(0, 255, 0)] = TileID.Dirt,
                [new Color(0, 0, 255)] = TileID.Cloud,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.WallType("AcropolisBrickWall"),
                [new Color(0, 255, 255)] = mod.WallType("AcropolisWall"),
                [new Color(0, 255, 0)] = WallID.Dirt,
                [new Color(0, 0, 255)] = WallID.Cloud,
                [new Color(255, 255, 255)] = -2, 
                [Color.Black] = -1			
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Acropolis"), colorToTile, mod.GetTexture("Worldgeneration/AcropolisWalls"), colorToWall, null, mod.GetTexture("Worldgeneration/AcropolisRoof"));

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 79, origin.Y + 86, (ushort)mod.TileType("AcropolisAltar"));
            NetMessage.SendObjectPlacment(-1, origin.X + 79, origin.Y + 87, (ushort)mod.TileType("AcropolisAltar"), 0, 0, -1, -1);

            return true;
        }
    }

    public class Equinox : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.TileType("GreedBrick"),
                [new Color(0, 255, 255)] = mod.TileType("DayCrystal"),
                [new Color(0, 255, 0)] = mod.TileType("NightCrystal"),
                [new Color(255, 255, 0)] = mod.TileType("DaybringerBrick"),
                [new Color(0, 0, 255)] = mod.TileType("NightcrawlerBrick"),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/EquinoxAltar"), colorToTile, null, null, null, mod.GetTexture("Worldgeneration/EquinoxAltarSlope"));

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 36, origin.Y + 39, mod.TileType("WormAltar"));
            NetMessage.SendObjectPlacment(-1, origin.X + 36, origin.Y + 39, mod.TileType("WormAltar"), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 30, origin.Y + 42, mod.TileType("StarAltar"));
            NetMessage.SendObjectPlacment(-1, origin.X + 30, origin.Y + 42, mod.TileType("StarAltar"), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 45, origin.Y + 42, mod.TileType("GravAltar"));
            NetMessage.SendObjectPlacment(-1, origin.X + 80, origin.Y + 88, mod.TileType("GravAltar"), 0, 0, -1, -1);

            return true;
        }
    }

    public class Pit : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(128, 128, 128)] = mod.TileType("PitStone"),
                [new Color(0, 0, 255)] = mod.TileType("PitBars"),
                [new Color(0, 255, 0)] = mod.TileType("PitBridge"),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 0, 255)] = mod.WallType("PitBarWall"),
                [new Color(255, 0, 0)] = mod.WallType("PitStoneWall"),
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            WorldUtils.Gen(origin, new Shapes.Rectangle(336, 145), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Actions.SetLiquid(0, 0),
                new Actions.SetSlope(0)
            }));

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Pit"), colorToTile, mod.GetTexture("Worldgeneration/PitWall"), colorToWall, mod.GetTexture("Worldgeneration/PitLava"), mod.GetTexture("Worldgeneration/PitSlope"));

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 281, origin.Y + 52, mod.TileType("Throne"));
            NetMessage.SendObjectPlacment(-1, origin.X + 281, origin.Y + 52, mod.TileType("Throne"), 0, 0, -1, -1);

            return true;
        }
    }
    
    public class PitTeaser : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(128, 128, 128)] = mod.TileType("PitStone"),
                [new Color(0, 0, 255)] = mod.TileType("PitBars"),
                [new Color(0, 255, 0)] = mod.TileType("PitBridge"),
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            WorldUtils.Gen(origin, new Shapes.Rectangle(90, 103), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Actions.SetSlope(0)
            }));

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/PitConstruction"), colorToTile);

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 35, origin.Y + 20, mod.TileType("Throne"));
            NetMessage.SendObjectPlacment(-1, origin.X + 30, origin.Y + 20, mod.TileType("Throne"), 0, 0, -1, -1);

            return true;
        }
    }

    public class Parthenan : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance;


            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = mod.TileType("FulguritePlatingS"),
                [new Color(255, 0, 0)] = mod.TileType("FulguriteBrickS"),
                [new Color(0, 0, 255)] = mod.TileType("StormCloud"),
                [new Color(255, 0, 255)] = mod.TileType("FulgurGlassS"),
                [new Color(150, 150, 150)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = mod.WallType("FulguritePlatingWallS"),
                [new Color(255, 0, 255)] = mod.TileType("FulgurGlassWall"),
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Parthenan"), colorToTile, mod.GetTexture("Worldgeneration/ParthenanWalls"), colorToWall);
            
            gen.Generate(origin.X, origin.Y, true, true);
            WorldGen.PlaceObject(origin.X + 34, origin.Y + 47, (ushort)mod.TileType("DataBank"));
            WorldGen.PlaceChest(origin.X + 32, origin.Y + 47, (ushort)mod.TileType("StormChest"), true);
            WorldGen.PlaceChest(origin.X + 41, origin.Y + 47, (ushort)mod.TileType("StormChest"), true);
            return true;
        }
    }

    public class BOTE : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.TileType("RottedDynastyWoodS"),
                [new Color(0, 255, 0)] = mod.TileType("RottedPlatform"),
                //colorToTile[new Color(0, 0, 255)] = TileID.Rope;
                [new Color(0, 255, 255)] = mod.TileType("CthulhuPortal"),
                [new Color(255, 255, 0)] = TileID.Sand,
                [new Color(150, 150, 150)] = -2,
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.WallType("RottedFence"),
                [new Color(255, 255, 0)] = mod.WallType("RottedWall"),
                [new Color(255, 255, 255)] = mod.WallType("RottedWall"),
                [new Color(0, 255, 255)] = mod.WallType("RottedWall"),
                [new Color(255, 0, 255)] = mod.WallType("RottedWall"),
                [new Color(0, 255, 0)] = mod.WallType("RottedWall"),
                [new Color(0, 0, 255)] = WallID.Sail,
                [new Color(150, 150, 150)] = -2,
                [Color.Black] = -1 //don't touch when genning				
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Ship"), colorToTile, mod.GetTexture("Worldgeneration/ShipWalls"), colorToWall, mod.GetTexture("Worldgeneration/ShipWater"));

			int newOriginX = origin.X - (gen.width / 2);
			int newOriginY = origin.Y - (gen.height / 2) + 10;
            gen.Generate(newOriginX, newOriginY, true, true);
            
            //WorldGen.PlaceChest(newOriginX + 130, newOriginY + 102, (ushort)mod.TileType("SunkenChest"), true);
            return true;
        }
    }

    public class Crystal : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = TileID.CrystalBlock,
                [new Color(0, 0, 255)] = TileID.GraniteBlock,
                [new Color(255, 255, 255)] = -2, //turn into air
                [Color.Black] = -1 //don't touch when genning		
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = WallID.Crystal,
                [new Color(255, 255, 255)] = -2,
                [Color.Black] = -1
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/EnderCrystal"), colorToTile, mod.GetTexture("Worldgeneration/EnderCrystalWall"), colorToWall, null, mod.GetTexture("Worldgeneration/EnderCrystalSlope"));

            gen.Generate(origin.X, origin.Y, true, true);

            WorldGen.PlaceObject(origin.X + 27, origin.Y + 26, (ushort)mod.TileType("EnderMemory"));
            NetMessage.SendObjectPlacment(-1, origin.X + 27, origin.Y + 26, (ushort)mod.TileType("EnderMemory"), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 16, origin.Y + 27, (ushort)mod.TileType("CrystalChandelier"));
            NetMessage.SendObjectPlacment(-1, origin.X + 16, origin.Y + 27, (ushort)mod.TileType("CrystalChandelier"), 0, 0, -1, -1);
            WorldGen.PlaceObject(origin.X + 41, origin.Y + 27, (ushort)mod.TileType("CrystalChandelier"));
            NetMessage.SendObjectPlacment(-1, origin.X + 41, origin.Y + 27, (ushort)mod.TileType("CrystalChandelier"), 0, 0, -1, -1);

            return true;
        }
    }

    public class Keep : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;
            int worldSize = GetWorldSize();


            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(128, 128, 128)] = mod.TileType("KeepBrick"),
                [new Color(64, 64, 64)] = mod.TileType("TerraBrick"),
                [new Color(0, 128, 0)] = mod.TileType("TerraCrystal"),
                [new Color(255, 0, 0)] = mod.TileType("TerraDoor"),
                [new Color(255, 255, 0)] = mod.TileType("TerraGate"),
                [new Color(0, 64, 0)] = mod.TileType("TerraPillar"),
                [new Color(128, 0, 0)] = mod.TileType("TerraWoodSolid"),
                [new Color(0, 255, 255)] = mod.TileType("TerraWood"),
                [new Color(0, 0, 64)] = mod.TileType("TerraLeaves"),
                [new Color(64, 0, 0)] = mod.TileType("ScorchedShinglesS"),
                [new Color(255, 0, 255)] = mod.TileType("TerraVault"),
                [new Color(0, 0, 255)] = TileID.Glass,
                [new Color(255, 255, 255)] = -1, //don't touch when genning
                [Color.Black] = -2 //turn into air
            };

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
            {
                [new Color(0, 255, 0)] = mod.WallType("KeepWall"),
                [new Color(255, 0, 0)] = WallID.GreenStainedGlass,
                [new Color(255, 0, 255)] = WallID.PurpleStainedGlass,
                [new Color(0, 0, 255)] = WallID.Glass,
                [new Color(255, 255, 255)] = -1, //don't touch when genning
                [Color.Black] = -2 //turn into air
            };

            WorldUtils.Gen(origin, new Shapes.Rectangle(280, 230), Actions.Chain(new GenAction[] //remove all fluids in sphere...
			{
                new InWorld(),
                new Actions.SetLiquid(0, 0),
                new Actions.SetSlope(0)
            }));

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/LostKeep"), colorToTile, mod.GetTexture("Worldgeneration/LostKeepWall"), colorToWall, null, mod.GetTexture("Worldgeneration/LostKeepSlope"));

            int genX = origin.X;
            int genY = origin.Y;

            gen.Generate(genX, genY, true, true);

            Dictionary<Color, int> colorToTile2 = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = ModContent.TileType<Placeholder1>(),
                [new Color(0, 255, 0)] = ModContent.TileType<Placeholder2>(),
                [new Color(255, 0, 255)] = ModContent.TileType<Placeholder3>(),
                [Color.Black] = -1 //don't touch when genning
            };

            Texture2D platTex = mod.GetTexture("Worldgeneration/LostKeepPlatforms");

            TexGen gen2 = BaseWorldGenTex.GetTexGenerator(platTex, colorToTile2, null, null, null, null);
            gen2.Generate(genX, genY, true, true);

            int x;
            int y;

            for (x = origin.X; x < origin.X + platTex.Width; x++)
            {
                for (y = origin.Y; y < origin.Y + platTex.Height; y++)
                {
                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder1>())
                    {
                        Main.tile[x, y].ClearTile();
                        WorldGen.PlaceTile(x, y, ModContent.TileType<KeepPlatform>(), true, false, -1, 0);
                        WorldGen.SlopeTile(x, y, 1);
                    }
                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder2>())
                    {
                        Main.tile[x, y].ClearTile();
                        WorldGen.PlaceTile(x, y, ModContent.TileType<KeepPlatform>(), true, false, -1, 0);
                        WorldGen.SlopeTile(x, y, 2);
                    }
                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder3>())
                    {
                        Main.tile[x, y].ClearTile();
                        WorldGen.PlaceTile(x, y, ModContent.TileType<KeepPlatform>(), true, false, -1, 0);
                    }
                }
            }

            Texture2D ObjectTex = mod.GetTexture("Worldgeneration/LostKeepObjects");

            Dictionary<Color, int> colorToObj = new Dictionary<Color, int>
            {
                [new Color(255, 0, 0)] = mod.TileType("Placeholder1"),
                [new Color(0, 255, 0)] = mod.TileType("Placeholder2"),
                [new Color(0, 0, 255)] = mod.TileType("Placeholder3"),
                [new Color(128, 128, 128)] = mod.TileType("Placeholder4"),
                [new Color(64, 64, 64)] = mod.TileType("Placeholder5"),
                [new Color(255, 255, 0)] = mod.TileType("Placeholder6"),
                [new Color(128, 0, 0)] = mod.TileType("Placeholder7"),
                [new Color(0, 255, 255)] = mod.TileType("Placeholder8"),
                [new Color(128, 128, 0)] = mod.TileType("Placeholder9"),
                [new Color(0, 128, 128)] = mod.TileType("Placeholder10"),
                [new Color(128, 0, 128)] = mod.TileType("Placeholder11"),
                [new Color(0, 0, 128)] = mod.TileType("Placeholder12"),
                [new Color(0, 128, 0)] = mod.TileType("Placeholder13"),
                [new Color(64, 0, 64)] = mod.TileType("Placeholder14"),
                [new Color(0, 0, 64)] = mod.TileType("Placeholder15"),
                [new Color(64, 64, 0)] = mod.TileType("Placeholder16"),
                [new Color(64, 0, 0)] = mod.TileType("Placeholder17"),
                [Color.Black] = -1
            };

            TexGen gen3 = BaseWorldGenTex.GetTexGenerator(ObjectTex, colorToObj, null, null, null, null);
            gen3.Generate(genX, genY, true, true);

            for (x = origin.X; x < origin.X + ObjectTex.Width; x++)
            {
                for (y = origin.Y; y < origin.Y + ObjectTex.Height; y++)
                {
                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder1>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<KeepLamp>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder2>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<KeepLantern>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder3>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<KeepChandelier>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder4>())
                    {
                        Main.tile[x, y].ClearTile();
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder5>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<KeepTable>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder6>())
                    {
                        Main.tile[x, y].ClearTile();
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder7>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<KeepBookcase>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder8>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<TerraStatue>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder9>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<TerraBed>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder10>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<TerraBath>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder11>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<TerraSink>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder12>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<TerraChandelier>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder13>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<TerraLantern>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder14>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<RazewoodLantern>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder15>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<RazewoodBed>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder16>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, ModContent.TileType<RazewoodDresser>());
                    }

                    if (Main.tile[x, y].type == ModContent.TileType<Placeholder17>())
                    {
                        Main.tile[x, y].ClearTile();
                        GenUtils.ObjectPlace(x, y, TileID.CookingPots);
                    }
                }
            }

            WorldGen.PlaceTile(origin.X + 32, origin.Y + 137, mod.TileType("InvokerBookTile"), true, false);
            WorldGen.PlaceTile(origin.X + 36, origin.Y + 137, mod.TileType("InvokerBookTile"), true, false);

            #region Terra's Bedroom

            WorldGen.PlaceChest(origin.X + 238, origin.Y + 104, (ushort)mod.TileType("TerraDresser"));
            WorldGen.PlaceTile(origin.X + 226, origin.Y + 104, mod.TileType("TerraBookcase"), true, false);

            #endregion

            #region doors 

            WorldGen.PlaceTile(origin.X + 97, origin.Y + 60, mod.TileType("KeepDoor1"), true, false);
            WorldGen.PlaceTile(origin.X + 192, origin.Y + 60, mod.TileType("KeepDoor1"), true, false);
            WorldGen.PlaceTile(origin.X + 209, origin.Y + 60, mod.TileType("KeepDoor1"), true, false);
            WorldGen.PlaceTile(origin.X + 223, origin.Y + 60, mod.TileType("KeepDoor1"), true, false);

            WorldGen.PlaceTile(origin.X + 31, origin.Y + 152, mod.TileType("KeepDoor1"), true, false);
            WorldGen.PlaceTile(origin.X + 27, origin.Y + 162, mod.TileType("KeepDoor1"), true, false);
            WorldGen.PlaceTile(origin.X + 29, origin.Y + 183, mod.TileType("KeepDoor1"), true, false);

            WorldGen.PlaceTile(origin.X + 247, origin.Y + 152, mod.TileType("KeepDoor1"), true, false);
            WorldGen.PlaceTile(origin.X + 251, origin.Y + 162, mod.TileType("KeepDoor1"), true, false);
            WorldGen.PlaceTile(origin.X + 249, origin.Y + 183, mod.TileType("KeepDoor1"), true, false);

            WorldGen.PlaceTile(origin.X + 62, origin.Y + 80, mod.TileType("KeepDoor1"), true, false);

            WorldGen.PlaceTile(origin.X + 80, origin.Y + 130, mod.TileType("KeepDoor2"), true, false);
            WorldGen.PlaceTile(origin.X + 80, origin.Y + 158, mod.TileType("KeepDoor2"), true, false);

            WorldGen.PlaceTile(origin.X + 209, origin.Y + 130, mod.TileType("KeepDoor2"), true, false);
            WorldGen.PlaceTile(origin.X + 209, origin.Y + 158, mod.TileType("KeepDoor2"), true, false);

            WorldGen.PlaceTile(origin.X + 73, origin.Y + 130, mod.TileType("KeepDoor2"), true, false);
            WorldGen.PlaceTile(origin.X + 216, origin.Y + 102, mod.TileType("KeepDoor2"), true, false);

            WorldGen.PlaceTile(origin.X + 127, origin.Y + 114, mod.TileType("KeepDoor2S"), true, false);
            WorldGen.PlaceTile(origin.X + 160, origin.Y + 114, mod.TileType("KeepDoor2S"), true, false);

            WorldGen.PlaceTile(origin.X + 73, origin.Y + 109, mod.TileType("KeepDoor3"), true, false);
            WorldGen.PlaceTile(origin.X + 228, origin.Y + 108, mod.TileType("KeepDoor3"), true, false);

            #endregion

            #region Paintings

            WorldGen.PlaceTile(origin.X + 115, origin.Y + 79, ModContent.TileType<ShenPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 127, origin.Y + 79, ModContent.TileType<CRajahPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 139, origin.Y + 79, ModContent.TileType<IZPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 151, origin.Y + 79, ModContent.TileType<SoCPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 163, origin.Y + 79, ModContent.TileType<MushmadPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 175, origin.Y + 79, ModContent.TileType<DecayPainting>(), true, false);

            WorldGen.PlaceTile(origin.X + 103, origin.Y + 90, ModContent.TileType<FulgurusPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 133, origin.Y + 90, ModContent.TileType<AkumaPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 156, origin.Y + 90, ModContent.TileType<YamataPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 186, origin.Y + 90, ModContent.TileType<ZeroPainting>(), true, false);

            WorldGen.PlaceTile(origin.X + 103, origin.Y + 100, ModContent.TileType<DaedalusPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 119, origin.Y + 100, ModContent.TileType<HotJPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 135, origin.Y + 100, ModContent.TileType<AnubisPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 154, origin.Y + 100, ModContent.TileType<ValkyriePainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 170, origin.Y + 100, ModContent.TileType<NKPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 186, origin.Y + 100, ModContent.TileType<LuciferPainting>(), true, false);

            WorldGen.PlaceTile(origin.X + 89, origin.Y + 110, ModContent.TileType<RajahPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 105, origin.Y + 110, ModContent.TileType<UmbraPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 121, origin.Y + 110, ModContent.TileType<GreedPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 168, origin.Y + 110, ModContent.TileType<AcropolisPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 184, origin.Y + 110, ModContent.TileType<SanguinePainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 200, origin.Y + 110, ModContent.TileType<ShipPainting>(), true, false);

            WorldGen.PlaceTile(origin.X + 31, origin.Y + 87, ModContent.TileType<WizardPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 30, origin.Y + 136, ModContent.TileType<TerraPainting>(), true, false);
            WorldGen.PlaceTile(origin.X + 230, origin.Y + 102, ModContent.TileType<KingQueenPainting>(), true, false);

            #endregion

            #region Biomite Core Arena

            WorldGen.PlaceTile(origin.X + 144, origin.Y + 134, ModContent.TileType<CoreActivator>(), true, false);

            WorldGen.PlaceTile(origin.X + 140, origin.Y + 125, ModContent.TileType<Core>(), true, false);
            WorldGen.PlaceTile(origin.X + 106, origin.Y + 129, ModContent.TileType<Core>(), true, false);
            WorldGen.PlaceTile(origin.X + 174, origin.Y + 129, ModContent.TileType<Core>(), true, false);
            WorldGen.PlaceTile(origin.X + 113, origin.Y + 151, ModContent.TileType<Core>(), true, false);
            WorldGen.PlaceTile(origin.X + 167, origin.Y + 151, ModContent.TileType<Core>(), true, false);
            WorldGen.PlaceTile(origin.X + 140, origin.Y + 156, ModContent.TileType<Core>(), true, false);



            #endregion

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

    public class GenUtils
    {
        public static void ObjectPlace(Point Origin, int x, int y, int TileType)
        {
            WorldGen.PlaceObject(Origin.X + x, Origin.Y + y, TileType);
            NetMessage.SendObjectPlacment(-1, Origin.X + x, Origin.Y + y, TileType, 0, 0, -1, -1);
        }
        public static void ObjectPlace(int x, int y, int TileType)
        {
            WorldGen.PlaceObject(x, y, TileType);
            NetMessage.SendObjectPlacment(-1, x, y, TileType, 0, 0, -1, -1);
        }
    }

    public class RadialDitherTopMiddle2 : GenAction
	{
        private readonly int _width;
        private readonly float _innerRadius, _outerRadius;

		public RadialDitherTopMiddle2(int width, float innerRadius, float outerRadius)
		{
			_width = width;
			_innerRadius = innerRadius;
			_outerRadius = outerRadius;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			Vector2 value = new Vector2((float)origin.X + (_width / 2), origin.Y);
			Vector2 value2 = new Vector2(x, y);
			float num = Vector2.Distance(value2, value);
			float num2 = Math.Max(0f, Math.Min(1f, (num - _innerRadius) / (_outerRadius - _innerRadius)));
			if (_random.NextDouble() > num2)
			{
				return UnitApply(origin, x, y, args);
			}
			return Fail();
		}
	}	

	public class InWorld : GenAction
	{
		public InWorld()
		{
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			if(x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
				return Fail();
			return UnitApply(origin, x, y, args);
		}
	}	
}