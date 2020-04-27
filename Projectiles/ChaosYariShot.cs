using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AAMod.Projectiles
{
    public class ChaosYariShot : AAProjectile
    {
        public bool spineEnd = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Yari");
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
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
        }

        public override void AI()
        {
            BaseAI.AIVilethorn(projectile, 80, 4, 20);
        }

        public override void PostAI()
        {
            if (Main.netMode != 2 && projectile.alpha < 170 && projectile.alpha + 5 >= 170)
            {
                for (int j = 0; j < 4; j++)
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, Main.rand.Next(2) == 0 ? Terraria.ModLoader.ModContent.DustType<Dusts.AkumaDust>() : Terraria.ModLoader.ModContent.DustType<Dusts.YamataAuraDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                }
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Color newLightColor = new Color(Math.Max(0, Color.Orange.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.Orange.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.Orange.B + Math.Min(0, -projectile.alpha + 20)));
            Color newLightColor2 = new Color(Math.Max(0, Color.Indigo.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.Indigo.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.Indigo.B + Math.Min(0, -projectile.alpha + 20)));
            BaseDrawing.AddLight(projectile.Center, newLightColor);
            BaseDrawing.AddLight(projectile.Center, newLightColor2);
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile);
            return false;
        }
    }
}