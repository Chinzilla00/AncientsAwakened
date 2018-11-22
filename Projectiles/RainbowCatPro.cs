using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class RainbowCatPro : ModProjectile
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Projectiles/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
            DisplayName.SetDefault("Legendary Rainbow Cat");
            Main.projFrames[projectile.type] = 17;
        }
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 46;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.alpha = 20;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 17)
                {
                    projectile.frame = 7;
                }
            }
            if (projectile.localAI[0] > 21f) //projectile time left before disappears
            {
                if (Main.rand.Next(3) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), ProjectileID.Meowmere, projectile.damage, 3, Main.myPlayer);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), ProjectileID.Meowmere, projectile.damage, 3, Main.myPlayer);
                }
                if (Main.rand.Next(50) == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), ProjectileID.RainbowRodBullet, projectile.damage, 3, Main.myPlayer);
                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y += 0.00f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 300f) //projectile time left before disappears
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 58, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 59, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 64, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 65, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 58, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 59, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 60, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 61, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 62, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 64, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 65, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 20, default(Color), 2f);
                projectile.Kill();
            }
        }
    }
}