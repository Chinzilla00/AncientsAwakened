using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Anubis
{
    public class Block : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 208;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (projectile.frame != 5)
            {
                if (projectile.frameCounter++ > 3)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
            }

            if (projectile.ai[0] == 0)
            {
                if (projectile.velocity.X < 12)
                {
                    projectile.velocity.X += .5f;
                }
            }
            else if (projectile.ai[0] == 1)
            {
                if (projectile.velocity.X > -12)
                {
                    projectile.velocity.X -= .5f;
                }
                projectile.direction = projectile.spriteDirection = -1;
            }
            else if (projectile.ai[0] == 2)
            {
                if (projectile.velocity.Y < 12)
                {
                    projectile.velocity.Y += .5f;
                }
            }
            else if (projectile.ai[0] == 3)
            {
                if (projectile.velocity.Y > -12)
                {
                    projectile.velocity.Y -= .5f;
                }
                projectile.direction = projectile.spriteDirection = -1;
            }

            Projectile clearCheck = Main.projectile[(int)projectile.ai[1]];
            if (Collision.CheckAABBvAABBCollision(projectile.position, projectile.Size, clearCheck.position, clearCheck.Size))
            {
                clearCheck.Kill();
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int m = 0; m < 40; m++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Sandstorm, 0f, 0f, 100, default, 1.6f);
            }
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}