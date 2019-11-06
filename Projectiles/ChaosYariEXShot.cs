using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ChaosYariEXShot : ModProjectile
    {
        public bool spineEnd = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfect Chaos Yari");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.timeLeft = 320;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.melee = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }

        public override void AI()
        {

            BaseMod.BaseAI.AIVilethorn(projectile, 70, 4, 30);
            spineEnd = projectile.ai[1] == 30;
            if (spineEnd)
            {
                projectile.frame = 0;
            }
            else
            {
                projectile.frame = 1;
            }
        }

        public override void PostAI()
        {
            if (Main.netMode != 2 && projectile.alpha < 170 && projectile.alpha + 5 >= 170)
            {
                for (int j = 0; j < 4; j++)
                {
                    int DustType = ModContent.DustType<Dusts.Discord>();
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, DustType, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f, 40, Color.White, j == 0 ? 1.1f : 1.2f);
                }
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Color newLightColor = new Color(Math.Max(0, Color.Purple.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.Purple.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.Purple.B + Math.Min(0, -projectile.alpha + 20)));
            BaseMod.BaseDrawing.AddLight(projectile.Center, newLightColor);
            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 3, 0, 2);
            BaseMod.BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}