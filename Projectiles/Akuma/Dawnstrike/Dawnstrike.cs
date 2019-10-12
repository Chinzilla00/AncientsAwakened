using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;

namespace AAMod.Projectiles.Akuma.Dawnstrike
{
    public class Dawnstrike : ModProjectile
    {
        public int counter = 0;
		public int chargeLevel = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dawnstrike");
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 74;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
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
			if (projectile.ai[1] >= num3 - num4 * num2)
			{
				projectile.ai[1] = 0f;
				flag = true;
			}
			if (projectile.ai[1] == 1f && projectile.ai[0] != 1f)
			{
				Vector2 vector2 = Vector2.UnitX * 24f;
				vector2 = vector2.RotatedBy(projectile.rotation - 1.57079637f, default);
				Vector2 value = projectile.Center + vector2;
				for (int i = 0; i < chargeLevel; i++)
				{
                    int type = chargeLevel >= 3 ? ModContent.DustType<Dusts.AkumaADust>() : ModContent.DustType<Dusts.AkumaDust>();
                    int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, type, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 100);
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
						value2.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector3.Y;
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
			if (player.direction == 1)
				projectile.Center = player.Center + new Vector2(10, 0);
			if (player.direction == -1)
				projectile.Center = player.Center + new Vector2(-18, 0);
			projectile.rotation = projectile.velocity.ToRotation() + num;
			projectile.spriteDirection = projectile.direction;
			projectile.timeLeft = 2;
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);

			counter++;

            if (counter >= 120)
            {
                GlowColor = AAColor.AkumaA;
                chargeLevel = 3;
            }

            else if (counter > 60)
            {
                GlowColor = Color.Goldenrod;
                chargeLevel = 2;
            }

            else if (counter <= 60)
            {
                chargeLevel = 1;
            }

            if (!player.channel)
			{
				projectile.Kill();
			}
        }


        public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
            int type;
            float damage = projectile.damage;
            if (chargeLevel == 3)
            {
                type = ModContent.ProjectileType<FireA>();
                damage = projectile.damage * 2;
            }
            else if (chargeLevel == 2)
            {
                type = ModContent.ProjectileType<AMeteor>();
                damage = projectile.damage * 1.5f;
            }
            else
            {
                type = ModContent.ProjectileType<MeteorF>();
            }
            if (projectile.owner == Main.myPlayer)
            {
				float num1 = 12f;
				Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f, player.position.Y + player.height * 0.5f);
				float f1 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float f2 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (player.gravDir == -1.0)
					f2 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
				float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
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
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 89);
                Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, type, (int)damage, 1f, player.whoAmI);
            }
        }

        public Color GlowColor = AAColor.Akuma;

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Dawnstrike_Glow");
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 1, frame, lightColor, true);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 1, frame, GlowColor, true);
            return false;
        }
    }
}
