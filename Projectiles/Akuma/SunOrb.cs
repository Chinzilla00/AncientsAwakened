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
            projectile.hide = true;
            projectile.sentry = true;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            float num1058 = 1000f;
            projectile.velocity = Vector2.Zero;
            
            projectile.alpha -= 5;
            if (projectile.alpha < 30)
            {
                    projectile.alpha = 30;
            }
            if (projectile.direction == 0)
            {
                    projectile.direction = Main.player[projectile.owner].direction;
            }
            projectile.rotation -= (float)projectile.direction * 6.28318548f / 120f;
            projectile.scale = projectile.Opacity;
            Lighting.AddLight(projectile.Center, new Vector3(0.3f, 0.9f, 0.7f) * projectile.Opacity);
            if (Main.rand.Next(2) == 0)
            {
                Vector2 vector135 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                Dust dust31 = Main.dust[Dust.NewDust(projectile.Center - vector135 * 30f, 0, 0, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default(Color), 1f)];
                dust31.noGravity = true;
                dust31.position = projectile.Center - vector135 * (float)Main.rand.Next(10, 21);
                dust31.velocity = vector135.RotatedBy(1.5707963705062866, default(Vector2)) * 6f;
                dust31.scale = 0.5f + Main.rand.NextFloat();
                dust31.fadeIn = 0.5f;
                dust31.customData = projectile.Center;
            }
            if (Main.rand.Next(2) == 0)
            {
                Vector2 vector136 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
                Dust dust32 = Main.dust[Dust.NewDust(projectile.Center - vector136 * 30f, 0, 0, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default(Color), 1f)];
                dust32.noGravity = true;
                dust32.position = projectile.Center - vector136 * 30f;
                dust32.velocity = vector136.RotatedBy(-1.5707963705062866, default(Vector2)) * 3f;
                dust32.scale = 0.5f + Main.rand.NextFloat();
                dust32.fadeIn = 0.5f;
                dust32.customData = projectile.Center;
            }
            if (projectile.ai[0] < 0f)
            {
                Vector2 center15 = projectile.Center;
                int num1059 = Dust.NewDust(center15 - Vector2.One * 8f, 16, 16, mod.DustType<Dusts.AkumaADust>(), projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
                Main.dust[num1059].velocity *= 2f;
                Main.dust[num1059].noGravity = true;
                Main.dust[num1059].scale = Utils.SelectRandom<float>(Main.rand, new float[]
                {
                    0.8f,
                    1.65f
                });
                Main.dust[num1059].customData = this;
            }
            if (projectile.ai[0] < 0f)
            {
                projectile.ai[0] += 1f;
                
                    projectile.ai[1] -= (float)projectile.direction * 0.3926991f / 50f;
                
            }
            if (projectile.ai[0] == 0f)
            {
                int num1060 = -1;
                float num1061 = num1058;
                NPC ownerMinionAttackTargetNPC6 = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC6 != null && ownerMinionAttackTargetNPC6.CanBeChasedBy(this, false))
                {
                    float num1062 = projectile.Distance(ownerMinionAttackTargetNPC6.Center);
                    if (num1062 < num1061 && Collision.CanHitLine(projectile.Center, 0, 0, ownerMinionAttackTargetNPC6.Center, 0, 0))
                    {
                        num1061 = num1062;
                        num1060 = ownerMinionAttackTargetNPC6.whoAmI;
                    }
                }
                if (num1060 < 0)
                {
                    for (int num1063 = 0; num1063 < 200; num1063++)
                    {
                        NPC nPC15 = Main.npc[num1063];
                        if (nPC15.CanBeChasedBy(this, false))
                        {
                            float num1064 = projectile.Distance(nPC15.Center);
                            if (num1064 < num1061 && Collision.CanHitLine(projectile.Center, 0, 0, nPC15.Center, 0, 0))
                            {
                                num1061 = num1064;
                                num1060 = num1063;
                            }
                        }
                    }
                }
                if (num1060 != -1)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = (float)num1060;
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
                float num1066 = 30f;
                if (projectile.ai[0] >= num1066)
                {
                    Vector2 vector137 = projectile.DirectionTo(Main.npc[num1065].Center);
                    if (vector137.HasNaNs())
                    {
                        vector137 = Vector2.UnitY;
                    }
                    float num1067 = vector137.ToRotation();
                    int num1068 = (vector137.X > 0f) ? 1 : -1;
                    projectile.direction = num1068;
                    projectile.ai[0] = -60f;
                    projectile.ai[1] = num1067 + (float)num1068 * 3.14159274f / 16f;
                    projectile.netUpdate = true;
                    if (projectile.owner == Main.myPlayer)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector137.X, vector137.Y, mod.ProjectileType("Sunray"), projectile.damage, projectile.knockBack, projectile.owner, 0f, (float)projectile.whoAmI);
                    }
                    
                }
            }
        }

        public float Rotation = 0;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 64, 64);
            Rotation += .0008f;
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Projectiles/Akuma/SunOrb"), 0, projectile.position + new Vector2(0, projectile.gfxOffY), projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, SunFrame, Color.White, true);
            return false;
        }
    }
}
