using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Infinity
{
    public class Anhialation : ModProjectile
	{
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Anhialation");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 48;
			projectile.aiStyle = 1;
			projectile.friendly = true;  
			projectile.hostile = false;       
			projectile.ranged = true;
			projectile.penetrate = 2;
            projectile.timeLeft = 600;
			projectile.alpha = 100;           
			projectile.light = 0.5f;         
			projectile.ignoreWater = true;
			projectile.tileCollide = false;        
			projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
            projectile.alpha = 30;           
		}


        public override void Kill(int timeleft)
        {

            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("AnhialationBurst"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }



        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(Color.White) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
