using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Snow
{
	public class SnakeTail : SnakeHead
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snow Serpent");
            Main.npcFrameCount[npc.type] = 5;
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