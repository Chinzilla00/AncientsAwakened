using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria;
using AAMod.Projectiles.Dev;

namespace AAMod
{
    public class ShockwaveBoom : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shockwave Boom");
        }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            float progress = (180f - projectile.timeLeft) / 60f;
            float pulseCount = 1;
            float rippleSize = 1;
            float speed = 20;
            if (projectile.ai[0] > 0)
            {
                rippleSize = projectile.ai[0];
            }
            if (projectile.ai[1] > 0)
            {
                speed = projectile.ai[1];
            }
            if (!Main.dedServ)
            {
                Filters.Scene["AAMod:Shockwave"].GetShader().UseProgress(progress).UseOpacity(100f * (1 - progress / 3f));
                projectile.localAI[1]++;
                if (projectile.localAI[1] >= 0 && projectile.localAI[1] <= 60)
                {
                    if (!Filters.Scene["AAMod:Shockwave"].IsActive())
                    {                                                             //pulseCount rippleSize speed
                        Filters.Scene.Activate("AAMod:Shockwave", projectile.Center).GetShader().UseColor(pulseCount, rippleSize, speed).UseTargetPosition(projectile.Center);
                    }
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            if (!Main.dedServ)
            {
                Filters.Scene["AAMod:Shockwave"].Deactivate();
            }
        }
    }

    public class ShockwaveWorld : ModWorld
    {
        public override void PostUpdate()
        {
            if (!Main.dedServ && Filters.Scene["AAMod:Shockwave"].IsActive() && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<ShockwaveBoom>()) && !Globals.AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<TemporalAnomalyEX>()))
            {
                Filters.Scene["AAMod:Shockwave"].Deactivate();
            }
        }
    }
}
