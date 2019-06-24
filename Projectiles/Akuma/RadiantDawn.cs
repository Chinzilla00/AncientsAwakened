using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Projectiles.Akuma
{
    public class RadiantDawn : ModProjectile
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
			if (projectile.ai[1] >= (num3 - num4 * num2))
			{
				projectile.ai[1] = 0f;
				flag = true;
			}
			if (projectile.ai[1] == 1f && projectile.ai[0] != 1f)
			{
				Vector2 vector2 = Vector2.UnitX * 24f;
				vector2 = vector2.RotatedBy(projectile.rotation - 1.57079637f, default(Vector2));
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
					Vector2 value2 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - vector3;
					if (player.gravDir == -1f)
					{
						value2.Y = (Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
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
			player.itemRotation = (float)Math.Atan2((projectile.velocity.Y * projectile.direction), (projectile.velocity.X * projectile.direction));

			counter++;

            if (counter >= 60)
            {
                chargeLevel = 2;
            }
            else if (counter >= 40)
            {
                chargeLevel = 1;
            }
            else if (counter >= 20)
            {
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
                Item item = player.HeldItem;
                int type = item.shoot;
                float speedX = 14;
                float speedY = 14;
                float num117 = 0.314159274f;
                Vector2 vector7 = new Vector2(speedX, speedY);
                vector7.Normalize();
                vector7 *= 40f;
                float num1 = 12f;
				Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
                bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
                float f1 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float f2 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (player.gravDir == -1.0)
					f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
				float num4 = (float)Math.Sqrt(f1 * f1 + f2 * f2);
				float num5;
				if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
				{
					f1 = player.direction;
					f2 = 0.0f;
					num5 = num1;
				}
				else
					num5 = num1 / num4;
				float SpeedX = f1 * num5;
				float SpeedY = f2 * num5;
                int num118 = 1;
                switch (chargeLevel)
                {
                    case 0:
						Main.PlaySound(SoundID.Item20, projectile.Center);
                        num118 = 1;

                        break;
					case 1:
						Main.PlaySound(SoundID.Item20, projectile.Center);
                        num118 = 3;
                        break;
					case 2:
						Main.PlaySound(SoundID.Item20, projectile.Center);
                        num118 = 6;
                        break;
                }
                for (int num119 = 0; num119 < num118; num119++)
                {
                    float num120 = num119 - (num118 - 1f) / 2f;
                    Vector2 value9 = vector7.RotatedBy((num117 * num120), default(Vector2));
                    if (!flag11)
                    {
                        value9 -= vector7;
                    }
                    int num121 = Projectile.NewProjectile(vector2.X + value9.X, vector2.Y + value9.Y, SpeedX, SpeedY, type, projectile.damage, 1f, player.whoAmI, 0.0f, 0.0f);
                    Main.projectile[num121].noDropItem = true;
                }
            }
        }
    }
}
