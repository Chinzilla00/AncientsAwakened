using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DragonfireDart : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragonfire Dart");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(477);
			projectile.penetrate = 1;
			projectile.aiStyle = 1; 
			projectile.extraUpdates = 1;
			aiType = 477;           
		}
		
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red;
        }

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCHit, (int)projectile.position.X, (int)projectile.position.Y, 7);
			for (int h = 0; h < 5; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.3f;
				vel = vel.RotatedBy(rand);
				vel *= 4f;
				int type = Main.rand.Next(326,328);
				int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vel.X, vel.Y, type, projectile.damage, 0, Main.myPlayer);
				Main.projectile[proj].localNPCHitCooldown = -1;
				Main.projectile[proj].timeLeft = 30;
				Main.projectile[proj].hostile = false;
				Main.projectile[proj].friendly = true;
			}
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
			target.AddBuff(mod.DustType("DragonFire"), 180);
		}
	}
}