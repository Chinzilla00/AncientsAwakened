using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles
{
    public class GlacierBreaker : ModProjectile
    {
		public bool stop = false;
		
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BlueMoon);
            projectile.penetrate = -1;  
            projectile.width = 26;
            projectile.height = 26;
			projectile.tileCollide = false;
			projectile.scale = 1.5f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            ProjectileDraw.DrawChain(projectile.whoAmI, Main.player[projectile.owner].MountedCenter,
                "AAMod/Projectiles/GlacierBreaker_Chain");
            ProjectileDraw.DrawAroundOrigin(projectile.whoAmI, lightColor);
            return false;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!stop)
			{
				Player player = Main.player[projectile.owner];
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num75 = 10f;
				float num82 = projectile.Center.X - vector2.X;
				float num83 = projectile.Center.Y - vector2.Y;
				float num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
				float num85 = num84;
				if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
				{
					num82 = projectile.direction;
					num83 = 0f;
					num84 = 11f;
				}
				else
				{
					num84 = 11f / num84;
				}
				num82 *= num84;
				num83 *= num84;
				int num117 = 6;
				for (int num118 = 0; num118 < num117; num118++)
				{
					vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (projectile.Center.X - player.position.X), projectile.Center.Y - 600f);
					vector2.X = (vector2.X + player.Center.X) / 2f + Main.rand.Next(-350, 351);
					vector2.Y -= 100 * num118;
					num82 = projectile.Center.X - vector2.X;
					num83 = projectile.Center.Y - vector2.Y;
					float ai2 = num83 + vector2.Y;
					if (num83 < 0f)
					{
						num83 *= -1f;
					}
					if (num83 < 20f)
					{
						num83 = 20f;
					}
					num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
					num84 = num75 / num84;
					num82 *= num84;
					num83 *= num84;
					Vector2 vector11 = new Vector2(num82, num83) / 2f;
					int p = Projectile.NewProjectile(vector2.X, vector2.Y, vector11.X*3f, vector11.Y*3f, 337, projectile.damage/3, 0f, player.whoAmI);
					Main.projectile[p].usesLocalNPCImmunity = true;
					Main.projectile[p].localNPCHitCooldown = 1;
					Main.projectile[p].tileCollide = false;
					Main.projectile[p].magic = false;
					Main.projectile[p].melee = true;
				}
				stop = true;
			}
		}
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacier Breaker");
        }
    }
}
