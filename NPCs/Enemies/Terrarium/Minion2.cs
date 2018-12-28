using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class Minion2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Crawler");
			Main.npcFrameCount[npc.type] = 5;
		}

		public override void SetDefaults()
		{
            npc.lifeMax =  350;
            npc.defense = 20;
            npc.damage = 50;
            npc.width = 26;
            npc.height = 20;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;
        }

        public override void AI()
        {
            BaseAI.AIZombie(npc, ref npc.ai, false, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);

            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 20;
                if (npc.frame.Y > (20 * 4))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
        }
    }
}
