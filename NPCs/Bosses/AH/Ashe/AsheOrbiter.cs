using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using BaseMod;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
	public class AsheOrbiter : Ashe
	{				
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Flame Vortex");
            Main.npcFrameCount[npc.type] = 4;
		}

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.width = 50;
            npc.height = 50;
            npc.friendly = false;
            npc.lifeMax = 1000;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.alpha = 255;
            npc.scale = .02f;
            npc.chaseable = false;
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
            if (npc.scale > 1f)
            {
                npc.scale = 1f;
            }
            else
            {
                npc.scale += .02f;
            }

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
				int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType("Ashe"), 120f, null);	
				if(npcID >= 0) body = npcID;
			}
			if(body == -1) return;
            
			NPC ashe = Main.npc[body];
			if(ashe == null || ashe.life <= 0 || !ashe.active || ashe.type != mod.NPCType("Ashe")){ BaseAI.KillNPCWithLoot(npc); return; }
            
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            if (rotValue == -1f) rotValue = (npc.ai[0] % OrbiterCount) * ((float)Math.PI * 2f / OrbiterCount);
            rotValue += 0.05f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
            npc.Center = BaseUtility.RotateVector(ashe.Center, ashe.Center + new Vector2(140f, 0f), rotValue);
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            damage = 0;
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            damage = 0;
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
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), mod.ProjectileType<AsheMagicSpark>(), npc.damage / 2, 0, Main.myPlayer, 0f, 0f);
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