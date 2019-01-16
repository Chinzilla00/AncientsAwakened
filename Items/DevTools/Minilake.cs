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
		public override void SetStaticDefaults()
		{	
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Generates a Mire Biome below you", "'Careful not to use it near your house!'" });					
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
            item.autoReuse = false;
            item.consumable = true;	
        }

		public override bool UseItem(Player player)
		{
            Point origin = new Point((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f));
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            MireBiome biome = new MireBiome();
            biome.Place(origin, WorldGen.structures);
            return true;
		}

		public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}