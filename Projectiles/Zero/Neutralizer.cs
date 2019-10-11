using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class Neutralizer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 800;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
        }

		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Death Beam");
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -oldVelocity.X;
                projectile.damage = (int)(projectile.damage * 1.2);
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -oldVelocity.Y;
                projectile.damage = (int)(projectile.damage * 1.2);
            }
            if (projectile.damage > 3000 * player.rangedDamage)
            {
                projectile.damage = (int)(3000 * player.rangedDamage);
            }
            return false; // return false because we are handling collision
        }

        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                for (int num447 = 0; num447 < 4; num447++)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * (num447 * 0.25f);
                    projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, projectile.width, projectile.height, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 200); //Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 200);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.2f;
                    Main.dust[num448].noGravity = true;
                }
            }
        }

    }
}
