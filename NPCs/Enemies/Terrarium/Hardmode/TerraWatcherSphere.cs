using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Terrarium.Hardmode
{
    public class TerraWatcherSphere : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 0;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.timeLeft = 800;
            projectile.penetrate = 10;
            projectile.tileCollide = true;
            projectile.aiStyle = 1;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Sphere");
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

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
            projectile.penetrate -= 1;

            if (projectile.penetrate <= 0)
            {
                return true;
            }
            return false;
        }

        public override void PostAI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                for (int num447 = 0; num447 < 4; num447++)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * (num447 * 0.25f);
                    projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, projectile.width, projectile.height, mod.DustType<Dusts.SummonDust>(), 0f, 0f, 200, default(Color), 1f);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.2f;
                    Main.dust[num448].noGravity = true;
                }
            }
        }

    }
}
