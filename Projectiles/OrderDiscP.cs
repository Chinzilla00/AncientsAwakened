using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class OrderDiscP : ModProjectile
    {
		public static int defense = 0;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(106);
			projectile.melee = false;
            projectile.ranged = true;
            projectile.penetrate = -1;  
            projectile.width = 22;
            projectile.height = 32;
			projectile.aiStyle = 3;
			aiType = 106;
        }

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Order Disc");
		}
		
		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 211,
				projectile.velocity.X * .5f, projectile.velocity.Y * .5f, 200, Scale: 1.1f);
				dust.velocity += projectile.velocity * 0.4f;
				dust.velocity *= 0.3f;
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			defense = target.defense;
			target.defense = 0;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;
			target.defense = defense;
		}
    }
}
