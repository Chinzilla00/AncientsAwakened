using BaseMod;
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
            Rotation += .0008f;
            float num1058 = 1000f;
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
            Vector2 vector46 = projectile.position;
            if (projectile.ai[0] == 0f)
            {
                int num1060 = -1;
                NPC ownerMinionAttackTargetNPC6 = projectile.OwnerMinionAttackTargetNPC;
                for (int num645 = 0; num645 < 200; num645++)
                {
                    NPC nPC2 = Main.npc[num645];
                    if (nPC2.CanBeChasedBy(projectile, false))
                    {
                        float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                        if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num1058) || projectile.ai[0] <= 0f) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                        {
                            num1058 = num646;
                            vector46 = nPC2.Center;
                            num1060 = nPC2.whoAmI;
                        }
                    }
                }
                if (num1060 != -1)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = num1060;
                    projectile.netUpdate = true;
                    return;
                }
            }
            if (projectile.ai[0] > 0f)
            {
                int num1065 = (int)projectile.ai[1];
                if (!Main.npc[num1065].CanBeChasedBy(this, false))
                {
                    projectile.ai[0] = 0f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                    return;
                }
                projectile.ai[0] += 1f;
                float num1066 = 60f;
                if (projectile.ai[0] >= num1066)
                {
                    if (projectile.ai[0] % 10 == 0)
                    {
                        float scaleFactor3 = 8f;
                        int num658 = mod.ProjectileType<FlamingMeteor>();
                        if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, projectile.position, 0, 0))
                        {
                            Vector2 value19 = projectile.position - projectile.Center;
                            value19.Normalize();
                            value19 *= scaleFactor3;
                            Vector2 perturbedSpeed = value19.RotatedByRandom(MathHelper.ToRadians(30));
                            int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, num658, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num659].timeLeft = 300;
                            projectile.netUpdate = true;
                        }
                    }
                }
                else if (projectile.ai[0] >= 91)
                {
                    projectile.ai[0] = 0;
                }
            }
        }
    }
}
