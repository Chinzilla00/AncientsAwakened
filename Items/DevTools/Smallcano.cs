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
	public class Smallcano : ModItem
	{
		public override void SetStaticDefaults()
		{	
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Generates a Volcano below you", "'Careful not to use it near your house!'" });					
		}		
		
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.rare = 10;
            item.value = BaseMod.BaseUtility.CalcValue(0, 0, 0, 0);

			item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;	
        }

		public override bool UseItem(Player player)
		{
            Mod mod = AAMod.instance;
            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(255, 0, 0)] = mod.TileType("Torchstone");
            colorToTile[new Color(0, 0, 255)] = mod.TileType("Torchstone");
            colorToTile[new Color(150, 150, 150)] = -2; //turn into air
            colorToTile[Color.Black] = -1; //don't touch when genning

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
            colorToWall[new Color(255, 0, 0)] = mod.WallType("TorchstoneWall");
            colorToWall[Color.Black] = -1; //don't touch when genning		

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/Volcano"), colorToTile, mod.GetTexture("Worldgeneration/VolcanoWalls"), colorToWall, mod.GetTexture("Worldgeneration/VolcanoLava"));
            Point origin = new Point((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            gen.Generate(origin.X, origin.Y - 40, true, true);
            return true;
		}

		public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}