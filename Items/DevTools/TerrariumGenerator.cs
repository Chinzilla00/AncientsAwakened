using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.Worldgeneration;
using System.Linq;

namespace AAMod.Items.DevTools
{
	public class TerrariumGenerator : ModItem
	{
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("Terrarium");
            Tooltip.SetDefault(@"Spawns a Terrarium in the heart of your world
You have this item because either
A: This is an old world w/o AA worldgen in it
B: This thing this item is for failed to spawn.
No need to worry. Just use this item and you'll be good as new.
Careful though, this may lag your game for a few moments.");				
		}		
		
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 10;
            item.value = BaseUtility.CalcValue(0, 0, 0, 0);

			item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.autoReuse = false;
            item.consumable = true;	
        }

		public override bool UseItem(Player player)
		{
            Point origin = new Point((int)(Main.maxTilesX * 0.5f), (int)(Main.maxTilesY * 0.4f)); ;
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            TerrariumDelete delete = new TerrariumDelete();
            TerrariumSphere biome = new TerrariumSphere();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);

            AAWorld.TerrariumGenerated = true;
            return true;
        }

        public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }
	}
}