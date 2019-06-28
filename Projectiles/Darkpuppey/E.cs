using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.Projectiles.Darkpuppey
{
    public class E : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 15;
            GlowColor = Color.CornflowerBlue;
            AlphaInterval = 70;
            Debuff = 0;
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
    }
}