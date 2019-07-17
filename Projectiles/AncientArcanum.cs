using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class AncientArcanum : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Quazar");
        }
    	
        public override void SetDefaults()
        {
            projectile.width = 60;
			projectile.height = 60;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.hide = false;
			projectile.magic = true;
			projectile.penetrate = 3;
			projectile.usesLocalNPCImmunity = true;
			projectile.tileCollide = false;
        }

        public override void AI()
        {
			Vector2 velocity = projectile.velocity;
			if (projectile.velocity.X != velocity.X)
			{
				projectile.velocity.X = -velocity.X * 0.35f;
			}
			if (projectile.velocity.Y != velocity.Y)
			{
				projectile.velocity.Y = -velocity.Y * 0.35f;
			}
			float[] var_2_2DDF8_cp_0 = projectile.ai;
			int var_2_2DDF8_cp_1 = 0;
			float num73 = var_2_2DDF8_cp_0[var_2_2DDF8_cp_1];
			var_2_2DDF8_cp_0[var_2_2DDF8_cp_1] = num73 + 1f;
			int num1013 = 0;
			if (projectile.velocity.Length() <= 4f)
			{
				num1013 = 1;
			}
			projectile.alpha -= 15;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			if (num1013 == 0)
			{
				projectile.rotation -= 0.104719758f;
				if (Main.rand.Next(3) == 0)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector140 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust28 = Main.dust[Dust.NewDust(projectile.Center - vector140 * 30f, 0, 0, Utils.SelectRandom(Main.rand, new int[]
						{
							88,
							92
						}), 0f, 0f, 0, default, 1f)];
						dust28.noGravity = true;
						dust28.position = projectile.Center - vector140 * Main.rand.Next(10, 21);
						dust28.velocity = vector140.RotatedBy(1.5707963705062866, default) * 6f;
						dust28.scale = 0.5f + Main.rand.NextFloat();
						dust28.fadeIn = 0.5f;
						dust28.customData = projectile;
					}
					else
					{
						Vector2 vector141 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust29 = Main.dust[Dust.NewDust(projectile.Center - vector141 * 30f, 0, 0, 236, 0f, 0f, 0, default, 1f)];
						dust29.noGravity = true;
						dust29.position = projectile.Center - vector141 * 30f;
						dust29.velocity = vector141.RotatedBy(-1.5707963705062866, default) * 3f;
						dust29.scale = 0.5f + Main.rand.NextFloat();
						dust29.fadeIn = 0.5f;
						dust29.customData = projectile;
					}
				}
				if (projectile.ai[0] >= 30f)
				{
					projectile.velocity *= 0.98f;
					projectile.scale += 0.00744680827f;
					if (projectile.scale > 1.3f)
					{
						projectile.scale = 1.3f;
					}
					projectile.rotation -= 0.0174532924f;
				}
				if (projectile.velocity.Length() < 4.1f)
				{
					projectile.velocity.Normalize();
					projectile.velocity *= 4f;
					projectile.ai[0] = 0f;
				}
			}
			else if (num1013 == 1)
			{
				projectile.rotation -= 0.104719758f;
				int num3;
				for (int num1014 = 0; num1014 < 1; num1014 = num3 + 1)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector142 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust30 = Main.dust[Dust.NewDust(projectile.Center - vector142 * 30f, 0, 0, 88, 0f, 0f, 0, default, 1f)];
						dust30.noGravity = true;
						dust30.position = projectile.Center - vector142 * Main.rand.Next(10, 21);
						dust30.velocity = vector142.RotatedBy(1.5707963705062866, default) * 6f;
						dust30.scale = 0.9f + Main.rand.NextFloat();
						dust30.fadeIn = 0.5f;
						dust30.customData = projectile;
						vector142 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						dust30 = Main.dust[Dust.NewDust(projectile.Center - vector142 * 30f, 0, 0, 92, 0f, 0f, 0, default, 1f)];
						dust30.noGravity = true;
						dust30.position = projectile.Center - vector142 * Main.rand.Next(10, 21);
						dust30.velocity = vector142.RotatedBy(1.5707963705062866, default) * 6f;
						dust30.scale = 0.9f + Main.rand.NextFloat();
						dust30.fadeIn = 0.5f;
						dust30.customData = projectile;
						dust30.color = Color.Crimson;
					}
					else
					{
						Vector2 vector143 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
						Dust dust31 = Main.dust[Dust.NewDust(projectile.Center - vector143 * 30f, 0, 0, 236, 0f, 0f, 0, default, 1f)];
						dust31.noGravity = true;
						dust31.position = projectile.Center - vector143 * Main.rand.Next(20, 31);
						dust31.velocity = vector143.RotatedBy(-1.5707963705062866, default) * 5f;
						dust31.scale = 0.9f + Main.rand.NextFloat();
						dust31.fadeIn = 0.5f;
						dust31.customData = projectile;
					}
					num3 = num1014;
				}
				if (projectile.ai[0] % 30f == 0f && projectile.ai[0] < 241f && Main.myPlayer == projectile.owner)
				{
					Vector2 vector144 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * 12f;
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector144.X, vector144.Y, 618, projectile.damage / 2, 0f, projectile.owner, 0f, projectile.whoAmI);
				}
				Vector2 vector145 = projectile.Center;
				float num1015 = 800f;
				bool flag59 = false;
				int num1016 = 0;
				if (projectile.ai[1] == 0f)
				{
					for (int num1017 = 0; num1017 < 200; num1017 = num3 + 1)
					{
						if (Main.npc[num1017].CanBeChasedBy(projectile, false))
						{
							Vector2 center13 = Main.npc[num1017].Center;
							if (projectile.Distance(center13) < num1015 && Collision.CanHit(new Vector2(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2), 1, 1, Main.npc[num1017].position, Main.npc[num1017].width, Main.npc[num1017].height))
							{
								num1015 = projectile.Distance(center13);
								vector145 = center13;
								flag59 = true;
								num1016 = num1017;
							}
						}
						num3 = num1017;
					}
					if (flag59)
					{
						if (projectile.ai[1] != num1016 + 1)
						{
							projectile.netUpdate = true;
						}
						projectile.ai[1] = num1016 + 1;
					}
					flag59 = false;
				}
				if (projectile.ai[1] != 0f)
				{
					int num1018 = (int)(projectile.ai[1] - 1f);
					if (Main.npc[num1018].active && Main.npc[num1018].CanBeChasedBy(projectile, true) && projectile.Distance(Main.npc[num1018].Center) < 1000f)
					{
						flag59 = true;
						vector145 = Main.npc[num1018].Center;
					}
				}
				if (!projectile.friendly)
				{
					flag59 = false;
				}
				if (flag59)
				{
					float num1019 = 4f;
					int num1020 = 8;
					Vector2 vector146 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
					float num1021 = vector145.X - vector146.X;
					float num1022 = vector145.Y - vector146.Y;
					float num1023 = (float)Math.Sqrt(num1021 * num1021 + num1022 * num1022);
					num1023 = num1019 / num1023;
					num1021 *= num1023;
					num1022 *= num1023;
					projectile.velocity.X = (projectile.velocity.X * (num1020 - 1) + num1021) / num1020;
					projectile.velocity.Y = (projectile.velocity.Y * (num1020 - 1) + num1022) / num1020;
				}
			}
			if (projectile.alpha < 150)
			{
				Lighting.AddLight(projectile.Center, 0.7f, 0.2f, 0.6f);
			}
			if (projectile.ai[0] >= 600f)
			{
				projectile.Kill();
				return;
			}
        }

		public override Color? GetAlpha(Color newColor)
		{
			return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha);
		}

        public override void Kill(int timeLeft)
        {
            projectile.position = projectile.Center;
			projectile.width = (projectile.height = 176);
			projectile.Center = projectile.position;
			projectile.maxPenetrate = -1;
			projectile.penetrate = -1;
			projectile.Damage();
			Main.PlaySound(SoundID.Item14, projectile.position);
			int num3;
			for (int num95 = 0; num95 < 4; num95 = num3 + 1)
			{
				int num96 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 236, 0f, 0f, 100, default, 1.5f);
				Main.dust[num96].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				num3 = num95;
			}
			for (int num97 = 0; num97 < 30; num97 = num3 + 1)
			{
				int num98 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, 0f, 0f, 200, default, 3.7f);
				Main.dust[num98].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				Main.dust[num98].noGravity = true;
				Dust dust = Main.dust[num98];
				dust.velocity *= 3f;
				num98 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 92, 0f, 0f, 100, default, 1.5f);
				Main.dust[num98].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				dust = Main.dust[num98];
				dust.velocity *= 2f;
				Main.dust[num98].noGravity = true;
				Main.dust[num98].fadeIn = 1f;
				Main.dust[num98].color = Color.Crimson * 0.5f;
				num3 = num97;
			}
			for (int num99 = 0; num99 < 10; num99 = num3 + 1)
			{
				int num100 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, 0f, 0f, 0, default, 2.7f);
				Main.dust[num100].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default) * projectile.width / 2f;
				Main.dust[num100].noGravity = true;
				Dust dust = Main.dust[num100];
				dust.velocity *= 3f;
				num3 = num99;
			}
			for (int num101 = 0; num101 < 10; num101 = num3 + 1)
			{
				int num102 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 236, 0f, 0f, 0, default, 1.5f);
				Main.dust[num102].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default) * projectile.width / 2f;
				Main.dust[num102].noGravity = true;
				Dust dust = Main.dust[num102];
				dust.velocity *= 3f;
				num3 = num101;
			}
			for (int num103 = 0; num103 < 2; num103 = num3 + 1)
			{
				int num104 = Gore.NewGore(projectile.position + new Vector2(projectile.width * Main.rand.Next(100) / 100f, projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default, Main.rand.Next(61, 64), 1f);
				Main.gore[num104].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				Gore gore = Main.gore[num104];
				gore.velocity *= 0.3f;
				Gore expr_3C56_cp_0_cp_0 = Main.gore[num104];
				expr_3C56_cp_0_cp_0.velocity.X += Main.rand.Next(-10, 11) * 0.05f;
				Gore expr_3C81_cp_0_cp_0 = Main.gore[num104];
				expr_3C81_cp_0_cp_0.velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
				num3 = num103;
			}
			if (Main.myPlayer == projectile.owner)
			{
				for (int num105 = 0; num105 < 1000; num105 = num3 + 1)
				{
					if (Main.projectile[num105].active && Main.projectile[num105].type == 618 && Main.projectile[num105].ai[1] == projectile.whoAmI)
					{
						Main.projectile[num105].Kill();
					}
					num3 = num105;
				}
				int num106 = Main.rand.Next(5, 9);
				int num107 = Main.rand.Next(5, 9);
				int num108 = Utils.SelectRandom(Main.rand, new int[]
				{
					86,
					90
				});
				int num109 = (num108 == 86) ? 90 : 86;
				for (int num110 = 0; num110 < num106; num110 = num3 + 1)
				{
					Vector2 vector4 = projectile.Center + Utils.RandomVector2(Main.rand, -30f, 30f);
					Vector2 vector5 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					while (vector5.X == 0f && vector5.Y == 0f)
					{
						vector5 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					}
					vector5.Normalize();
					if (vector5.Y > 0.2f)
					{
						vector5.Y *= -1f;
					}
					vector5 *= Main.rand.Next(70, 101) * 0.1f;
					Projectile.NewProjectile(vector4.X, vector4.Y, vector5.X, vector5.Y, 620, (int)(projectile.damage * 0.65), projectile.knockBack * 0.8f, projectile.owner, num108, 0f);
					num3 = num110;
				}
				for (int num111 = 0; num111 < num107; num111 = num3 + 1)
				{
					Vector2 vector6 = projectile.Center + Utils.RandomVector2(Main.rand, -30f, 30f);
					Vector2 vector7 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					while (vector7.X == 0f && vector7.Y == 0f)
					{
						vector7 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
					}
					vector7.Normalize();
					if (vector7.Y > 0.4f)
					{
						vector7.Y *= -1f;
					}
					vector7 *= Main.rand.Next(40, 81) * 0.1f;
					Projectile.NewProjectile(vector6.X, vector6.Y, vector7.X, vector7.Y, 620, (int)(projectile.damage * 0.65), projectile.knockBack * 0.8f, projectile.owner, num109, 0f);
					num3 = num111;
				}
			}
        }

		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 vector53 = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
			Texture2D texture2D31 = Main.projectileTexture[projectile.type];
            Color color25 = new Color(250 - 5 * 10, 250 - 5 * 10, 250 - 5 * 10, 150 - 5 * 10);
			Color alpha4 = projectile.GetAlpha(color25);
			SpriteEffects spriteEffects = SpriteEffects.None;
			Vector2 origin8 = new Vector2(texture2D31.Width, texture2D31.Height) / 2f;
            Color color57 = alpha4 * 0.8f;
			color57.A /= 2;
			Color color58 = Color.Lerp(alpha4, Color.Black, 0.5f);
			color58.A = alpha4.A;
			float num274 = 0.95f + (projectile.rotation * 0.75f).ToRotationVector2().Y * 0.1f;
			color58 *= num274;
			float scale13 = 0.6f + projectile.scale * 0.6f * num274;
			SpriteBatch arg_DA77_0 = Main.spriteBatch;
			Texture2D arg_DA77_1 = Main.extraTexture[50];
			Vector2 arg_DA77_2 = vector53;
			Rectangle? sourceRectangle2 = null;
			arg_DA77_0.Draw(arg_DA77_1, arg_DA77_2, sourceRectangle2, color58, -projectile.rotation + 0.35f, origin8, scale13, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
			SpriteBatch arg_DAC3_0 = Main.spriteBatch;
			Texture2D arg_DAC3_1 = Main.extraTexture[50];
			Vector2 arg_DAC3_2 = vector53;
			sourceRectangle2 = null;
			arg_DAC3_0.Draw(arg_DAC3_1, arg_DAC3_2, sourceRectangle2, alpha4, -projectile.rotation, origin8, projectile.scale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
			SpriteBatch arg_DB13_0 = Main.spriteBatch;
			Texture2D arg_DB13_1 = texture2D31;
			Vector2 arg_DB13_2 = vector53;
			sourceRectangle2 = null;
			arg_DB13_0.Draw(arg_DB13_1, arg_DB13_2, sourceRectangle2, color57, -projectile.rotation * 0.7f, origin8, projectile.scale, spriteEffects ^ SpriteEffects.FlipHorizontally, 0f);
			SpriteBatch arg_DB72_0 = Main.spriteBatch;
			Texture2D arg_DB72_1 = Main.extraTexture[50];
			Vector2 arg_DB72_2 = vector53;
			sourceRectangle2 = null;
			arg_DB72_0.Draw(arg_DB72_1, arg_DB72_2, sourceRectangle2, alpha4 * 0.8f, projectile.rotation * 0.5f, origin8, projectile.scale * 0.9f, spriteEffects, 0f);
			alpha4.A = 0;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.immune[projectile.owner] = 1;
            projectile.Kill();
        }
    }
}