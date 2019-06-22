using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Accessories
{
    public class Snap : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snap");
        }
        public override void SetDefaults()
        {
            projectile.width = 1000;
            projectile.height = 1000;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.timeLeft = 500;
            projectile.scale = 1f;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Center;
            projectile.scale += 0.1f;
            if (projectile.localAI[0] == 1f)
            {
                projectile.alpha += 20;
                if (projectile.alpha >= 255)
                {
                    projectile.Kill();
                }
            }
            else
            {
                projectile.alpha -= 2;
                if (projectile.alpha <= 0)
                {
                    projectile.localAI[0] = 1f;
                }
            }
        }
    }
}