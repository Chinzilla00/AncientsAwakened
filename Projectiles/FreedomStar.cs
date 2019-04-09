using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class FreedomStar : ModProjectile
    {
        public int counter = 0;
		public int chargeLevel = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FreedomStar");
        }

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 74;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
			Player player = Main.player[projectile.owner];
			
			float num = 1.57079637f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			projectile.ai[0] += 1f;
			int num2 = 0;
			if (projectile.ai[0] >= 30f)
			{
				num2++;
			}
			if (projectile.ai[0] >= 60f)
			{
				num2++;
			}
			if (projectile.ai[0] >= 90f)
			{
				num2++;
			}
			int num3 = 24;
			int num4 = 6;
			projectile.ai[1] += 1f;
			bool flag = false;
			if (projectile.ai[1] >= (float)(num3 - num4 * num2))
			{
				projectile.ai[1] = 0f;
				flag = true;
			}
			if (projectile.ai[1] == 1f && projectile.ai[0] != 1f)
			{
				Vector2 vector2 = Vector2.UnitX * 24f;
				vector2 = vector2.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
				Vector2 value = projectile.Center + vector2;
				for (int i = 0; i < 3; i++)
				{
					int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, 74, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 100, default(Color), 1f);
					Main.dust[num5].position.Y -= 0.3f;
					Main.dust[num5].velocity *= 0.66f;
					Main.dust[num5].noGravity = true;
					Main.dust[num5].scale = 1.4f;
				}
			}
			if (flag && Main.myPlayer == projectile.owner)
			{
				if (player.channel && !player.noItems && !player.CCed)
				{
					float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
					Vector2 vector3 = vector;
					Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector3;
					if (player.gravDir == -1f)
					{
						value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
					}
					Vector2 vector4 = Vector2.Normalize(value2);
					if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
					{
						vector4 = -Vector2.UnitY;
					}
					vector4 *= scaleFactor;
					if (vector4.X != projectile.velocity.X || vector4.Y != projectile.velocity.Y)
					{
						projectile.netUpdate = true;
					}
					projectile.velocity = vector4;
					int num6 = 440;
					float scaleFactor2 = 14f;
					int num7 = 7;
				
					vector3 = projectile.Center + new Vector2((float)Main.rand.Next(-num7, num7 + 1), (float)Main.rand.Next(-num7, num7 + 1));
					Vector2 vector5 = Vector2.Normalize(projectile.velocity) * scaleFactor2;
					vector5 = vector5.RotatedBy(Main.rand.NextDouble() * 0.19634954631328583 - 0.098174773156642914, default(Vector2));
					if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
					{
						vector5 = -Vector2.UnitY;
					}
				}
			}
			projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
			projectile.rotation = projectile.velocity.ToRotation() + num;
			projectile.spriteDirection = projectile.direction;
			projectile.timeLeft = 2;
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));

			counter++;

            if (counter >= 90)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 93);
                chargeLevel = 3;
            }

            else if (counter >= 90)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 101);
                chargeLevel = 2;
            }

            else if (counter >= 30)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 13);
                chargeLevel = 1;
            }

            else if (counter >= 30)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 13);
                chargeLevel = 0;
            }

            if (!player.channel)
			{
				projectile.Kill();
			}
        }

        public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
            if (projectile.owner == Main.myPlayer)
            {
				float num1 = 12f;
				Vector2 vector2 = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
				float f1 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				float f2 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if ((double)player.gravDir == -1.0)
					f2 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
				float num4 = (float)Math.Sqrt((double)f1 * (double)f1 + (double)f2 * (double)f2);
				float num5;
				if (float.IsNaN(f1) && float.IsNaN(f2) || (double)f1 == 0.0 && (double)f2 == 0.0)
				{
					f1 = (float)player.direction;
					f2 = 0.0f;
					num5 = num1;
				}
				else
					num5 = num1 / num4;
				float SpeedX = f1 * num5;
				float SpeedY = f2 * num5;
                switch (chargeLevel)
                {
                    case 0:
						Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 89);
						Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, mod.ProjectileType("FreedomShot"), projectile.damage, 1f, player.whoAmI);
						break;
					case 1:
						Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 89);
                        Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, mod.ProjectileType("FreedomShot"), projectile.damage*2, 1f, player.whoAmI);
						break;
					case 2:
						Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 88);
                        Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, mod.ProjectileType("FreedomShot"), projectile.damage*2, 1f, player.whoAmI);
						break;
					case 3:
						Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 88);
                        Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, mod.ProjectileType("FreedomShotCharged"), projectile.damage*6, 1f, player.whoAmI);
						break;
                }
            }
        }
    }
}
