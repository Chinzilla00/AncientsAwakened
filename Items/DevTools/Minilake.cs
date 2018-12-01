using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using GRealm.WorldGeneration;

namespace GRealm.Items.Dev
{
	public class DevPNGGenerator : GItem
	{
		public override void SetStaticDefaults()
		{
            displayName = "Texture World Generator";	
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Forces a test texture-to-world generation below you", "'Only grealm devs or testers should use this!'" });					
		}		
		
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.rare = 10;
            item.value = BaseMod.BaseUtility.CalcValue(0, 0, 0, 0);

			item.useStyle = GRealm.USESTYLE_CUSTOM;
            item.useAnimation = 45;
            item.useTime = 45;
			devItem = true;		
        }

		public override bool UseItem(Player player)
		{
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[Color.White] = mod.TileType("GroviteBrick");
			colorToTile[new Color(150, 150, 150)] = -2; //turn into air
			colorToTile[Color.Black] = -1; //don't touch when genning

			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[Color.White] = mod.WallType("GroviteBrickWall");
			colorToWall[Color.Black] = -1; //don't touch when genning			
	
			TexGen gen = BaseWorldGenTex.GetTexGenerator(GRealm.GetTexture("worldgen"), colorToTile, GRealm.GetTexture("worldgen_walls"), colorToWall, GRealm.GetTexture("worldgen_liquid"));
			Point origin = new Point((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
			gen.Generate(origin.X, origin.Y + 2, true, true);
			return true;
		}

		public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}