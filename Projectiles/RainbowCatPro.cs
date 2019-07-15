using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class RainbowCatPro : ModProjectile
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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

                int Shoot = ProjectileID.Meowmere;
                Main.projectile[Shoot].usesLocalNPCImmunity = true;
                Main.projectile[Shoot].localNPCHitCooldown = 4;
                Main.projectile[Shoot].melee = false;
                Main.projectile[Shoot].magic = true;

                if (Main.myPlayer == projectile.owner)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), Shoot, projectile.damage, 3, Main.myPlayer);
                        int proj1 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), Shoot, projectile.damage, 3, Main.myPlayer);
                    }
                    if (Main.rand.Next(50) == 0)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -16 + Main.rand.Next(0, 33), -16 + Main.rand.Next(0, 33), ProjectileID.RainbowRodBullet, projectile.damage, 3, Main.myPlayer);
                    }
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