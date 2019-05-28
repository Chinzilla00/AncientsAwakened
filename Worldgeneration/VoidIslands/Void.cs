using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.World.Generation;
using BaseMod;

namespace AAMod.Worldgeneration
{
    public class VoidIslands : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = mod.TileType("OroborosWood");
            colorToTile[new Color(0, 255, 0)] = mod.TileType("Doomstone");
            colorToTile[new Color(0, 0, 255)] = mod.TileType("DoomstoneBricks");
            colorToTile[Color.Black] = -1; //don't touch when genning		

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = mod.WallType("OroborosWoodWall");
            colorToTile[new Color(0, 0, 255)] = mod.TileType("DoomstoneBrickWall");
            colorToWall[new Color(150, 150, 150)] = -2;
            colorToWall[Color.Black] = -1; //don't touch when genning			

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/BOTE"), colorToTile);

			int newOriginX = origin.X - (gen.width / 2);
			int newOriginY = origin.Y - (gen.height / 2) + 10;
            gen.Generate(newOriginX, newOriginY, true, true);
            return true;
        }
    }
}