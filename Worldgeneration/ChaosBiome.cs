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

namespace AAMod.Worldgeneration
{ 
	public class MireBiome : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			//this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.
			
			Mod mod = AAMod.instance; //replace with your own
			bool DEV = true;			
			//--- Initial variable creation
			ushort tileGrass = (ushort)mod.TileType("MireGrass"), tileJungleGrass = (ushort)mod.TileType("MireGrass"), tileDirt = TileID.Mud, tileStone = (ushort)mod.TileType("Depthstone"); //change to types in your mod

			int worldSize = GetWorldSize();
			int biomeRadius = (worldSize == 3 ? 180 : worldSize == 2 ? 150 : 120), biomeRadiusHalf = biomeRadius / 2; //how deep the biome is (scaled by world size)	
			
			//ok time to check to see if this spot is actually a good place to gen
			Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
			Point newOrigin = new Point(origin.X, origin.Y - 10); //biomeRadius);
			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), new Actions.TileScanner(new ushort[]
			{
				TileID.Grass,
                TileID.JungleGrass,
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
				if(Main.netMode == 0 && DEV) Main.NewText("NOT A GOOD SPOT! Counts - Dungeon: " + dungeonCount + ", Normal: " + normalBiomeCount + ", Ice: " + IceBlockBiomeCount + ", Sand: " + sandBiomeCount + ".");
				return false;
			}

			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new BaseMod.SetModTile(tileGrass, true, true) //actually place the tile
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.JungleGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new BaseMod.SetModTile(tileJungleGrass, true, true) //actually place the tile
			}));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Dirt }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileDirt, true, true)
			}));
			WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //and stone.
			{
				new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone }),
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
				new BaseMod.SetModTile(tileStone, true, true)
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

    public class InfernoBiome : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = AAMod.instance; //replace with your own
            bool DEV = true;
            //--- Initial variable creation
            ushort tileGrass = (ushort)mod.TileType("InfernoGrass"), tileJungleGrass = (ushort)mod.TileType("InfernoGrass"), tileDirt = TileID.Dirt, tileStone = (ushort)mod.TileType("Torchstone"); //change to types in your mod

            int worldSize = GetWorldSize();
            int biomeRadius = (worldSize == 3 ? 180 : worldSize == 2 ? 150 : 120), biomeRadiusHalf = biomeRadius / 2; //how deep the biome is (scaled by world size)	

            //ok time to check to see if this spot is actually a good place to gen
            Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
            Point newOrigin = new Point(origin.X, origin.Y - 10); //biomeRadius);
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), new Actions.TileScanner(new ushort[]
            {
                TileID.Grass,
                TileID.JungleGrass,
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
                if (Main.netMode == 0 && DEV) Main.NewText("NOT A GOOD SPOT! Counts - Dungeon: " + dungeonCount + ", Normal: " + normalBiomeCount + ", Ice: " + IceBlockBiomeCount + ", Sand: " + sandBiomeCount + ".");
                return false;
            }

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new BaseMod.SetModTile(tileGrass, true, true) //actually place the tile
			}));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //gen grass...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.JungleGrass }), //ensure we only replace the intended tile (in this case, grass)
				new Modifiers.RadialDither(biomeRadius - 5, biomeRadius), //this provides the 'blending' on the edges (except the top)
				new BaseMod.SetModTile(tileJungleGrass, true, true) //actually place the tile
			}));

            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //dirt...
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Dirt }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new BaseMod.SetModTile(tileDirt, true, true)
            }));
            WorldUtils.Gen(newOrigin, new Shapes.Circle(biomeRadius), Actions.Chain(new GenAction[] //and stone.
			{
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Stone }),
                new Modifiers.RadialDither(biomeRadius - 5, biomeRadius),
                new BaseMod.SetModTile(tileStone, true, true)
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

    public class RadialDitherTopMiddle2 : GenAction
	{
		private int _width, _height;
		private float _innerRadius, _outerRadius;

		public RadialDitherTopMiddle2(int width, int height, float innerRadius, float outerRadius)
		{
			this._width = width;
			this._height = height;
			this._innerRadius = innerRadius;
			this._outerRadius = outerRadius;
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