using BaseMod;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class SunOrb : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sun Portal");
            Main.projFrames[projectile.type] = 1;
        }

		public override void SetDefaults()
		{
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.sentry = true;
        }

        public float Rotation = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 64, 64);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Projectiles/Akuma/SunOrb1"), 0, projectile.position + new Vector2(0, projectile.gfxOffY), projectile.width, projectile.height, projectile.scale, -projectile.rotation, projectile.spriteDirection, 1, SunFrame, AAColor.COLOR_WHITEFADE1, true);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Projectiles/Akuma/SunOrb"), 0, projectile.position + new Vector2(0, projectile.gfxOffY), projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, SunFrame, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }

        public override void AI()
        {
			Player player = Main.player[projectile.owner];
            Rotation += .0008f;
            projectile.rotation += .0008f;
            projectile.velocity = Vector2.Zero;
            if (projectile.direction == 0)
            {
                    projectile.direction = Main.player[projectile.owner].direction;
            }
            projectile.rotation -= projectile.direction * 6.28318548f / 120f;
            projectile.scale = projectile.Opacity;
            Lighting.AddLight(projectile.Center, new Vector3(0.3f, 0.9f, 0.7f) * projectile.Opacity);
            if (Main.rand.Next(2) == 0)
            {
                Vector2 vector135 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                Dust dust31 = Main.dust[Dust.NewDust(projectile.Center - vector135 * 30f, 0, 0, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
                dust31.noGravity = true;
                dust31.position = projectile.Center - vector135 * Main.rand.Next(10, 21);
                dust31.velocity = vector135.RotatedBy(1.5707963705062866, default) * 6f;
                dust31.scale = 0.5f + Main.rand.NextFloat();
                dust31.fadeIn = 0.5f;
                dust31.customData = projectile.Center;
            }
            if (Main.rand.Next(2) == 0)
            {
                Vector2 vector136 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                Dust dust32 = Main.dust[Dust.NewDust(projectile.Center - vector136 * 30f, 0, 0, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default, 1f)];
                dust32.noGravity = true;
                dust32.position = projectile.Center - vector136 * 30f;
                dust32.velocity = vector136.RotatedBy(-1.5707963705062866, default) * 3f;
                dust32.scale = 0.5f + Main.rand.NextFloat();
                dust32.fadeIn = 0.5f;
                dust32.customData = projectile.Center;
            }
            if (projectile.ai[0] < 0f)
            {
                Vector2 center15 = projectile.Center;
                int num1059 = Dust.NewDust(center15 - Vector2.One * 8f, 16, 16, mod.DustType<Dusts.AkumaADust>(), projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0);
                Main.dust[num1059].velocity *= 2f;
                Main.dust[num1059].noGravity = true;
                Main.dust[num1059].scale = Utils.SelectRandom(Main.rand, new float[]
                {
                    0.8f,
                    1.65f
                });
                Main.dust[num1059].customData = this;
            }
            if (projectile.ai[0] < 0f)
            {
                projectile.ai[0] += 1f;
                
                    projectile.ai[1] -= projectile.direction * 0.3926991f / 50f;
                
            }
			
			float num633 = 700f;
            float num634 = 800f;
            float num635 = 1200f;
            float num636 = 150f;
            float num637 = 0.05f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == mod.ProjectileType("HallowedPrism");
                if (num638 != projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == projectile.owner && flag23 && Math.Abs(projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num638].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - num637;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num637;
                    }
                    if (projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num637;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num637;
                    }
                }
            }
            bool flag24 = false;
            if (projectile.ai[0] == 2f)
            {
                projectile.ai[1] += 1f;
                projectile.extraUpdates = 1;
                if (projectile.ai[1] > 40f)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.extraUpdates = 0;
                    projectile.numUpdates = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    flag24 = true;
                }
            }
            if (flag24)
            {
                return;
            }
            Vector2 vector46 = projectile.position;
            bool flag25 = false;
            if (projectile.ai[0] != 1f)
            {
                projectile.tileCollide = false;
            }
            if (projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16)))
            {
                projectile.tileCollide = false;
            }
            for (int num645 = 0; num645 < 200; num645++)
            {
                NPC nPC2 = Main.npc[num645];
                if (nPC2.CanBeChasedBy(projectile, false))
                {
                    float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                    if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                    {
                        num633 = num646;
                        vector46 = nPC2.Center;
                        flag25 = true;
                    }
                }
            }
            float num647 = num634;
            if (flag25)
            {
                num647 = num635;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > num647)
            {
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }
            if (flag25 && projectile.ai[0] == 0f)
            {
                Vector2 vector47 = vector46 - projectile.Center;
                float num648 = vector47.Length();
                vector47.Normalize();
                if (num648 > 200f)
                {
                    float scaleFactor2 = 8f;
                    vector47 *= scaleFactor2;
                    projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                }
                else
                {
                    float num649 = 4f;
                    vector47 *= -num649;
                    projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                }
            }
            else
            {
                bool flag26 = false;
                if (!flag26)
                {
                    flag26 = projectile.ai[0] == 1f;
                }
                float num650 = 5f; //6
                if (flag26)
                {
                    num650 = 12f; //15
                }
                Vector2 center2 = projectile.Center;
                Vector2 vector48 = player.Center - center2 + new Vector2(0f, -30f); //-60
                float num651 = vector48.Length();
                if (num651 > 200f && num650 < 6.5f) //200 and 8
                {
                    num650 = 6.5f; //8
                }
                if (num651 < num636 && flag26 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (num651 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width / 2;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height / 2;
                    projectile.netUpdate = true;
                }
                if (num651 > 70f)
                {
                    vector48.Normalize();
                    vector48 *= num650;
                    projectile.velocity = (projectile.velocity * 40f + vector48) / 41f;
                }
                else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.2f;
                    projectile.velocity.Y = -0.1f;
                }
            }
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (projectile.ai[1] > 30f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                int num658 = mod.ProjectileType<FlamingMeteor>();
                if (flag25 && projectile.ai[1] == 0f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
                    {
                        Vector2 value19 = vector46 - projectile.Center;
                        value19.Normalize();
                        value19 *= 8;
						Vector2 perturbedSpeed = value19.RotatedByRandom(MathHelper.ToRadians(10));
                        int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, num658, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num659].timeLeft = 300;
                        projectile.netUpdate = true;
                    }
                }
            }
        }
    }
}
