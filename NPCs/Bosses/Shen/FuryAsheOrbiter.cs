using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using BaseMod;

namespace AAMod.NPCs.Bosses.Shen
{
	public class FuryAsheOrbiter : FuryAshe
	{				
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Flame Vortex");
            Main.npcFrameCount[npc.type] = 4;
		}

        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 38;
            npc.value = BaseMod.BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 5000;
            npc.defense = 130;
            npc.damage = 5;
            npc.DeathSound = SoundID.DD2_BetsyFireballImpact;
            npc.knockBackResist = 0f;	
			npc.noTileCollide = true;
            npc.dontTakeDamage = true;
            npc.alpha = 255;
            npc.dontCountMe = true;
        }

		public int body = -1;
		public float rotValue = -1f;
        int NPCLife = 0;

		public override void AI()
		{
            
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            else
            {
                npc.alpha -= 4;
            }
			npc.noGravity = true;
			if(body == -1)
			{
				int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("FuryAshe"), 120f, null);	
				if(npcID >= 0) body = npcID;
			}
			if(body == -1) return;
            
			NPC ashe = Main.npc[body];
			if(ashe == null || ashe.life <= 0 || !ashe.active || ashe.type != mod.NPCType("FuryAshe")){ BaseAI.KillNPCWithLoot(npc); return; }


            npc.ai[2] = AIchange;

            if (npc.ai[2] > 300)
            {
                npc.ai[0] = 0;
                npc.ai[1] = 1;
                npc.ai = new float[4];
                npc.netUpdate = true;
            }

            if (npc.ai[1] != 1)
            {
                for (int m = npc.oldPos.Length - 1; m > 0; m--)
                {
                    npc.oldPos[m] = npc.oldPos[m - 1];
                }
                npc.oldPos[0] = npc.position;

                if (rotValue == -1f) rotValue = (npc.ai[0] % OrbiterCount) * ((float)Math.PI * 2f / OrbiterCount);
                rotValue += 0.05f;
                while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
                npc.Center = BaseUtility.RotateVector(ashe.Center, ashe.Center + new Vector2(OrbiterDistance, 0f), rotValue);
            }
            else
            {
                NPCLife++;
                if (NPCLife > 240)
                {
                    npc.life = 0;
                    npc.netUpdate = true;
                }
                BaseAI.AIElemental(npc, ref npc.ai, null, 1, false, false, 0, 0, 0, 5);
                npc.noGravity = true;
                npc.noTileCollide = true;
            }
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (npc.ai[1] == 1)
            {
                npc.life = 0;
            }
        }

        public override void NPCLoot()
        {
            float spread = 60f * 0.0174f;
            double startAngle = Math.Atan2(npc.velocity.X, -npc.velocity.Y) - spread / 2;
            double deltaAngle = spread / 6;
            double offsetAngle;
            for (int i = 0; i < 6; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), mod.ProjectileType<FuryAsheSpark>(), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			Color lightColor = BaseDrawing.GetNPCColor(npc, null);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, npc.GetAlpha(Color.White), true);
			return false;
		}		
	}
}