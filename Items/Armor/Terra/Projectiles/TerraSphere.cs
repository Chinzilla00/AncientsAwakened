using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra.Projectiles
{
    public class TerraSphere : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Bomb");
            Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.friendly = true; 
			projectile.hostile = false;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 20;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;          
		}

        public override void AI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }

            if (projectile.melee)
            {
                int foundTarget = (int)projectile.ai[1];
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * 30;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / 20);
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeleft)
        {
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            int p = Projectile.NewProjectile((int)projectile.Center.X, (int)projectile.Center.Y, 0, 0, ModContent.ProjectileType<TerraBoom>(), projectile.damage, projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = projectile.Center;
            for (int num468 = 0; num468 < 10; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 107, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
            
        }
    }
}
