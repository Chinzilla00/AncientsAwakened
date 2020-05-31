using AAMod.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class SunstormFireball : AAProjectile
	{
		float rot = 0f;
		float rotInit = -1f;
		bool released = false;
		
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.magic = true;
            projectile.ignoreWater = true;		
        }

		public void SetRot()
		{
			float oldInit = rotInit;
			int[] projs = BaseAI.GetProjectiles(Main.player[projectile.owner].Center, projectile.type, projectile.owner, 200f);
			rotInit = projs.Length == 0 ? 0f : ((float)Math.PI * 2f / projs.Length);

			if (rotInit != oldInit)
			{
				int projSlot = 0;
				for(int m = 0; m < projs.Length; m++)
				{
					if (projs[m] == projectile.identity) { projSlot = m; }
				}
				rot = rotInit * (projSlot + 1f);
			}
		}

        public override void AI()
        {
			projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }
			
			Player player = Main.player[projectile.owner];
			if (player == Main.player[projectile.owner])
			{
				if (player.altFunctionUse == 2)
				{
					released = true;
					float num1 = 12f;
					Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
					float f1 = Main.mouseX + Main.screenPosition.X - vector2.X;
					float f2 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
					if (player.gravDir == -1.0)
						f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
					float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
					float num5;
					if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
					{
						f1 = projectile.direction;
						f2 = 0.0f;
						num5 = num1;
					}
					else
						num5 = num1 / num4;
					float SpeedX = f1 * num5;
					float SpeedY = f2 * num5;
					projectile.velocity.X = SpeedX;
					projectile.velocity.Y = SpeedY;
				}
			}
			projectile.ai[0]++;
			if (projectile.ai[0] < 30 && !released)
            {
				if (projectile.active) { SetRot(); }
				BaseAI.AIRotate(projectile, ref projectile.rotation, ref rot, player.Center, true, 60f, 20f, 0.07f, true);
			}
			if (projectile.ai[0] >= 30)
            {
				int foundTarget1 = HomeOnTarget();
				if (!released)
				{
					if (foundTarget1 == -1)
					{
						if (projectile.active) { SetRot(); }
						BaseAI.AIRotate(projectile, ref projectile.rotation, ref rot, player.Center, true, 60f, 20f, 0.07f, true);
					}
				}
			}
            if (projectile.position.HasNaNs())
            {
                projectile.Kill();
                return;
            }
            bool flag5 = WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.position.X / 16, (int)projectile.position.Y / 16));
            Dust dust19 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust19.position = projectile.Center;
            dust19.velocity = Vector2.Zero;
            dust19.noGravity = true;
            Dust dust18 = Main.dust[Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
            dust18.position = projectile.Center;
            dust18.velocity = Vector2.Zero;
            dust18.noGravity = true;
            if (flag5)
            {
                dust19.noLight = true;
                dust18.noLight = true;
            }
            if (projectile.ai[1] == -1f)
            {
                projectile.velocity = Vector2.Zero;
                projectile.tileCollide = false;
                projectile.penetrate = -1;
                projectile.position = projectile.Center;
                projectile.width = projectile.height = 140;
                projectile.Center = projectile.position;
                return;
            }
            if (projectile.ai[0] > 30)
            {
                projectile.ai[0] = 30; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * 30;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / 30);
                }
            }
            if (projectile.numUpdates == 0)
            {
                int num185 = -1;
                float num186 = 60f;
                for (int num187 = 0; num187 < 200; num187++)
                {
                    NPC nPC2 = Main.npc[num187];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num188 = projectile.Distance(nPC2.Center);
                        if (num188 < num186 && Collision.CanHitLine(projectile.Center, 0, 0, nPC2.Center, 0, 0))
                        {
                            num186 = num188;
                            num185 = num187;
                        }
                    }
                }
                if (num185 != -1)
                {
                    projectile.ai[1] = -1f;
                    projectile.netUpdate = true;
                    return;
                }
				if (num185 == -1 && projectile.ai[1] == -1f)
                {
                    projectile.Kill();
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 300;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[1] = -1f;
            projectile.netUpdate = true;
        }

		public override void Kill(int timeLeft)
		{
			int[] projs = BaseAI.GetProjectiles(projectile.Center, projectile.type, projectile.owner, 200f);
			
			bool flag = WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.position.X / 16, (int)projectile.position.Y / 16));

            for (int num58 = 0; num58 < 4; num58++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 1.5f);
            }
            for (int num59 = 0; num59 < 4; num59++)
            {
                int num60 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 2.5f);
                Main.dust[num60].noGravity = true;
                Main.dust[num60].velocity *= 3f;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
                num60 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num60].velocity *= 2f;
                Main.dust[num60].noGravity = true;
                if (flag)
                {
                    Main.dust[num60].noLight = true;
                }
            }
		}
	}
}