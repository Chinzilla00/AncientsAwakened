using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Projectiles
{
    public class ChaosShot1 : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public int proType = 0;
        public override void SetStaticDefaults()
        {
           DisplayName.SetDefault("DNA");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
            projectile.alpha = 255;
            projectile.tileCollide = true;
        }

        public float vectorOffset = 0f;
        public bool offsetLeft = false;
        public Vector2 originalVelocity = Vector2.Zero;

        public override void AI()
        {
            int dustType = proType == 0 ? ModContent.DustType<Dusts.DiscordLight>() : proType == 1 ? ModContent.DustType<Dusts.AkumaDustLight>() : ModContent.DustType<Dusts.YamataDustLight>();
            if (projectile.ai[1] != 0)
            {
                projectile.extraUpdates = 1;
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 5;
            }
            else
            {
                projectile.penetrate = 1;
            }

            int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1) - projectile.velocity, 2, 2, dustType, 0f, 0f, 100, Color.White, 1.2f);
            Main.dust[dustID].velocity *= 0f;
            Main.dust[dustID].noLight = false;
            Main.dust[dustID].noGravity = true;

            if (originalVelocity == Vector2.Zero)
            {
                originalVelocity = projectile.velocity;
            }
            if (proType != 0)
            {
                if (offsetLeft)
                {
                    vectorOffset -= 0.08f;
                    if (vectorOffset <= -0.5f)
                    {
                        vectorOffset = -0.5f;
                        offsetLeft = false;
                    }
                }
                else
                {
                    vectorOffset += 0.08f;
                    if (vectorOffset >= 0.5f)
                    {
                        vectorOffset = 0.5f;
                        offsetLeft = true;
                    }
                }
                float velRot = BaseUtility.RotationTo(projectile.Center, projectile.Center + originalVelocity);
                projectile.velocity = BaseUtility.RotateVector(default, new Vector2(projectile.velocity.Length(), 0f), velRot + (vectorOffset * 0.5f));
            }
            projectile.rotation = BaseUtility.RotationTo(projectile.Center, projectile.Center + projectile.velocity) + 1.57f - MathHelper.PiOver4;
            projectile.spriteDirection = 1;
        }

        public override void Kill(int timeLeft)
        {
            int dustType = proType == 0 ? 0 : proType == 1 ? ModContent.DustType<Dusts.AkumaDustLight>() : ModContent.DustType<Dusts.YamataAuraDust>();
            if (proType != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, 0f, 0f, 100, default, 1.2f);
                    Main.dust[dustIndex].velocity *= 1.9f;
                }
            }
        }
    }
}