using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Darkpuppey
{
    public class E : ModProjectile
    {
        public bool spineEnd = false;
        public bool spineBeginning = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Decieving Truth");
            Main.projFrames[projectile.type] = 3;
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
            projectile.magic = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int n = 0; n < 3; n++)
            {
                float x = projectile.position.X + Main.rand.Next(-400, 400);
                float y = projectile.position.Y - Main.rand.Next(500, 800);
                Vector2 vector = new Vector2(x, y);
                float num13 = projectile.position.X + (projectile.width / 2) - vector.X;
                float num14 = projectile.position.Y + (projectile.height / 2) - vector.Y;
                num13 += Main.rand.Next(-100, 101);
                int num15 = 23;
                float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
                num16 = num15 / num16;
                num13 *= num16;
                num14 *= num16;
                int num17 = Projectile.NewProjectile(x, y, num13, num14, 92, 70, 5f, Main.myPlayer, 0f, 0f);
                Main.projectile[num17].ai[1] = projectile.position.Y;
            }
        }

        public override void AI()
        {
            BaseMod.BaseAI.AIVilethorn(projectile, 70, 4, 15);
            spineBeginning = projectile.ai[1] == 0;
            spineEnd = projectile.ai[1] == 15;
            if (projectile.ai[1] == 0)
            {
                projectile.frame = 2;
            }
            else if (projectile.ai[1] == 15)
            {
                projectile.frame = 0;
            }
            else
            {
                projectile.frame = 1;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Color newLightColor = new Color(Math.Max(0, Color.CornflowerBlue.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.CornflowerBlue.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.CornflowerBlue.B + Math.Min(0, -projectile.alpha + 20)));
            BaseMod.BaseDrawing.AddLight(projectile.Center, newLightColor);
            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 3, 0, 2);
            BaseMod.BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}