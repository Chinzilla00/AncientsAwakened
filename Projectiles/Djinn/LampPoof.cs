using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Djinn
{
    class LampPoof : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 100;
            projectile.friendly = true;
            projectile.hostile = false;
        }

        public override void AI()
        {
            projectile.velocity.Y += .3f;
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, mod.DustType<Dusts.SandDust>(), projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, mod.DustType<Dusts.SandDust>(), projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
            Main.dust[dustId3].noGravity = true;
        }
        
    }
}