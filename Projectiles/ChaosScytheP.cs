using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ChaosScytheP : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CHAOS CHAOS");
        }
    	
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.aiStyle = -1;
            projectile.alpha = 254;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }

        public bool CHAOSCHAOS = false;

        public override void AI()
        {
            if (CHAOSCHAOS == false && projectile.alpha > 0)
            {
                projectile.alpha -= 15;
            }
            if (CHAOSCHAOS == false && projectile.alpha <= 0)
            {
                projectile.alpha = 0;
                CHAOSCHAOS = true;
            }
            if (CHAOSCHAOS == true && projectile.alpha < 255)
            {
                projectile.alpha += 5;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = -1;
            }
            projectile.rotation += projectile.direction * 0.2f;
        }
    }
}