using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.NPCs.Enemies.Snow
{
	public class SnakeBody : SnakeHead
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