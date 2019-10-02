using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using BaseMod;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    public class AsheOrbiter : ModNPC
	{				
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Flame Vortex");
            Main.npcFrameCount[npc.type] = 4;
		}

        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.width = 46;
            npc.height = 46;
            npc.friendly = false;
            npc.lifeMax = 1300;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.alpha = 255;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

		public int body = -1;
		public float rotValue = -1f;
		public override void AI()
		{
            if (npc.frameCounter++ > 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 46;
                if (npc.frame.Y >= 46 * 4)
                {
                    npc.frame.Y = 0;
                }
            }

            if (npc.alpha > 0)
            {
                npc.alpha -= 4;
            }
            else
            {
                npc.alpha = 0;
            }
			npc.noGravity = true;
			if(body == -1)
			{
				int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("Ashe"), 120f, null);	
				if(npcID >= 0) body = npcID;
			}
			if(body == -1) return;
            
			NPC ashe = Main.npc[body];
			if(ashe == null || ashe.life <= 0 || !ashe.active || ashe.type != mod.NPCType("Ashe")){ npc.active = false; return; }
            
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;


            npc.ai[2] += 2 * (float)Math.PI / 420;
            if (npc.ai[2] > (float)Math.PI)
            {
                npc.ai[2] -= 2 * (float)Math.PI;
            }
            npc.ai[1] += (float)Math.Sin(npc.ai[2]) * 7;
            npc.position = ashe.Center + new Vector2(npc.ai[1], 0f).RotatedBy(npc.ai[3]);
            npc.position.X -= npc.width / 2;
            npc.position.Y -= npc.height / 2;
            float rotation = npc.ai[1] == 125f ? 0.03f : -0.01f;
            npc.ai[3] += rotation;
            if (npc.ai[3] > (float)Math.PI)
            {
                npc.ai[3] -= 2f * (float)Math.PI;
                npc.netUpdate = true;
            }

            npc.rotation = npc.ai[3] + (float)Math.PI / 2f;
        }

        public override void NPCLoot()
        {
            float spread = 60f * 0.0174f;
            double startAngle = Math.Atan2(npc.velocity.X, -npc.velocity.Y) - spread / 2;
            double deltaAngle = spread / 6;
            double offsetAngle;
            for (int i = 0; i < 6; i++)
            {
                offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), mod.ProjectileType<AsheMagicSpark>(), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, npc.GetAlpha(Color.White), true);
            return false;
        }
    }
}