using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Djinn
{
    public class SandSpray : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spray");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 6;
            projectile.extraUpdates = 2;
            projectile.magic = true;
        }

        public override void AI()
        {
        	Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0f) / 255f);
			projectile.scale -= 0.002f;
			if (projectile.scale <= 0f)
			{
				projectile.Kill();
			}
			if (projectile.ai[0] <= 3f)
			{
				projectile.ai[0] += 1f;
				return;
			}
			projectile.velocity.Y = projectile.velocity.Y + 0.075f;
			for (int i = 0; i < 3; i++)
			{
				float xPos = projectile.velocity.X / 3f * (float)i;
				float yPos = projectile.velocity.Y / 3f * (float)i;
				int eggroll = 14;
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X + (float)eggroll, projectile.position.Y + (float)eggroll), projectile.width - eggroll * 2, projectile.height - eggroll * 2, 269, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 0.1f;
				Main.dust[dustIndex].velocity += projectile.velocity * 0.5f;
				Dust sand = Main.dust[dustIndex];
				sand.position.X = sand.position.X - xPos;
				Dust sand2 = Main.dust[dustIndex];
				sand2.position.Y = sand2.position.Y - yPos;
			}
			if (Main.rand.Next(8) == 0)
			{
				int eggplant = 16;
				int dustIndex2 = Dust.NewDust(new Vector2(projectile.position.X + (float)eggplant, projectile.position.Y + (float)eggplant), projectile.width - eggplant * 2, projectile.height - eggplant * 2, 269, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[dustIndex2].velocity *= 0.25f;
				Main.dust[dustIndex2].velocity += projectile.velocity * 0.5f;
				return;
			}
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        	target.immune[projectile.owner] = 8;
        }
    }
}