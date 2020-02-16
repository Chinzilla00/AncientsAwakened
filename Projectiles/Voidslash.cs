using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Voidslash : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Valkyrie Slash");
			Main.projFrames[projectile.type] = 28;
		}
    	
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Arkhalis);
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
			projectile.netUpdate = true;
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float num = 0f;
			if (projectile.spriteDirection == -1)
			{
				num = 3.14159274f;
			}
			if (projectile.frameCounter > 2)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
			}
			if (projectile.frame >= Main.projFrames[projectile.type])
			{
				projectile.frame = 0;
			}
			projectile.soundDelay--;
			if (projectile.soundDelay <= 0)
			{
				Main.PlaySound(SoundID.Item1, projectile.Center);
				projectile.soundDelay = 12;
			}
			if (Main.myPlayer == projectile.owner)
			{
				if (player.channel && !player.noItems && !player.CCed)
				{
					float scaleFactor6 = 1f;
					if (player.inventory[player.selectedItem].shoot == projectile.type)
					{
						scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
					}
					Vector2 vector13 = Main.MouseWorld - vector;
					vector13.Normalize();
					if (vector13.HasNaNs())
					{
						vector13 = Vector2.UnitX * player.direction;
					}
					vector13 *= scaleFactor6;
					if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
					{
						projectile.netUpdate = true;
					}
					projectile.velocity = vector13;
				}
				else
				{
					projectile.Kill();
				}
			}
			Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
			Lighting.AddLight(vector14, 0.5f, 0.3f, 0.32f);
			if (Main.rand.Next(3) == 0)
			{
				int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 63, projectile.velocity.X, projectile.velocity.Y, 100, Color.Red, 2f);
				Main.dust[num30].noGravity = true;
				Main.dust[num30].position -= projectile.velocity;
			}
			projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
			projectile.rotation = projectile.velocity.ToRotation() + num;
			projectile.spriteDirection = projectile.direction;
			projectile.timeLeft = 2;
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
		}

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return projHitbox.Intersects(targetHitbox);
        }

        public override Color? GetAlpha(Color lightColor)
		{
			Color value = Color.Lerp(lightColor, Color.White, 0.85f);
			value.A = 128;
			return value * (1f - projectile.alpha / 255f);
		}
    }
}