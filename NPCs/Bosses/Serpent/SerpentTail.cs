using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
	public class SerpentTail : SerpentHead
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subzero Serpent");
	        Main.npcFrameCount[npc.type] = 1;		
		}
		
		public override void SetDefaults()
		{
            base.SetDefaults();
            npc.dontCountMe = true;
		}

		public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
}