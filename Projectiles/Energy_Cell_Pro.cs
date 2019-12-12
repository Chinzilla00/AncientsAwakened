using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Energy_Cell_Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        
		}

		public override void SetDefaults()
		{
			projectile.width = 4;               
			projectile.height = 4;              
			projectile.aiStyle = 1;             
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.ranged = true;           
			projectile.timeLeft = 600;          
			projectile.alpha = 255;
			projectile.light = 0f;            
			projectile.ignoreWater = true;
			projectile.tileCollide = true;        
			aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 18;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.Red;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 60);
		}
	}
}