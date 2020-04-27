using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class TheStride : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flamebrute");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.OnFire] = true;
			banner = npc.type;
            npc.width = 62;
            npc.height = 90;
            npc.damage = 12;
            npc.defense = 4;
            npc.lifeMax = 90;
        }
        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[npc.target];

                if (npc.frameCounter++ > 7)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = npc.frame.Y + frameHeight;
                }
                if (npc.frame.Y >= frameHeight * 6)
                {
                    npc.frame.Y = 0;
                    return;
                }
        }

        public override void AI()
		{
            npc.ai[0] = 0.2f;
            npc.ai[1] = 1.2f;
            npc.TargetClosest(false);
            Player player = Main.player[npc.target];
            Vector2 moveTo = player.Center + new Vector2(npc.spriteDirection * (npc.width * 4), 0);
            npc.TargetClosest(true);
            float accel2 = Math.Abs(npc.Center.X - player.Center.X) / 140;
            if (accel2 > 0.6f)
                accel2 = 0.6f;
            if (npc.Center.X < player.Center.X)
            {
                npc.velocity.X += npc.ai[0] * accel2;
            }

            if (npc.Center.X > player.Center.X)
            {
                npc.velocity.X -= npc.ai[0] * accel2;
            }

            if (Math.Abs(npc.velocity.X) == npc.ai[1])
                npc.velocity.X = npc.ai[1] * npc.spriteDirection;
            base.AI();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
            if(npc.life <= 0)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 45, mod.NPCType("TheStrideDeathAnimation"));
            }
		}
	}
}
