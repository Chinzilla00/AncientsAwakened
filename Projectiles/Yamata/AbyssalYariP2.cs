using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles.Yamata
{
    public class AbyssalYariP2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 75;
            projectile.height = 75;
            projectile.scale = 1f;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.timeLeft = 80;
            projectile.alpha = 255;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Yari");
        }

        public override void AI()
        {
            NPC target= Main.npc[(int)projectile.ai[1]];
            if(projectile.alpha >= 50)
            {
                projectile.alpha -= 5;
                projectile.rotation = projectile.DirectionTo(target.Center).ToRotation() + 0.25f * (float)Math.PI;
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation() + 0.25f * (float)Math.PI;
            }
            
            if(projectile.alpha <= 50 && projectile.alpha > 45)
            {
                projectile.velocity = projectile.DirectionTo(target.Center) * 12f;
            }
        }

        public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(mod.BuffType("Moonraze"), 500);
        }
    }
}
