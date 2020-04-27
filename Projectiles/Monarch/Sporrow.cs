
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Monarch
{
    public class Sporrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sporrow");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.arrow = true;
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.MushDust>(), -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default);
            }
            Projectile.NewProjectile(projectile.position, Vector2.Zero, mod.ProjectileType("SporeCloud"), projectile.damage, 0, Main.myPlayer);
        }
    }
}
