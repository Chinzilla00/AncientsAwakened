using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Ammo
{
    public class EventideBullet : ModProjectile
	{
		public override void SetStaticDefaults() 
                {
			DisplayName.SetDefault("Eventide Bullet");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;    
			ProjectileID.Sets.TrailingMode[projectile.type] = 1;        
		}

		public override void SetDefaults() {
			projectile.width = 10;               
			projectile.height = 8;              
			projectile.aiStyle = 1;             
			projectile.friendly = true;         
			projectile.hostile = false;        
			projectile.ranged = true;           
			projectile.penetrate = 50;           
			projectile.timeLeft = 600;          
			projectile.alpha = 255;             
			projectile.light = 0.5f;            
			projectile.ignoreWater = true;           
			projectile.tileCollide = true;          
			projectile.extraUpdates = 10;            
			aiType = ProjectileID.Bullet;      
		}
                     Vector2? initialPos = null;
                     Vector2? initialVel = null;
                     public override void AI()
                       {
                         if (initialPos == null && initialVel == null)
                          {
                            initialPos = projectile.position;
                            initialVel = projectile.velocity;
                          }
                       }

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
                       {
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++) {
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

            public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
          {
            target.AddBuff(mod.BuffType("Moonraze"), 500);

            if (target.defense < 300 && !target.boss)
            {
                damage += target.defense * 2;
            }
          }

           public override void Kill(int timeLeft)
            {
                if (initialPos != null && initialVel != null)
                {
                  Projectile.NewProjectile((Vector2)initialPos, (Vector2)initialVel, mod.ProjectileType("EventideBullet1"), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
	}
}