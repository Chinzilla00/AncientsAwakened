using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Athena
{
    public class AthenaHurricane : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornado");
		}

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
            projectile.magic = true;
        }

        public override void AI()
        {
			float num1125 = 600f;
			if (projectile.soundDelay == 0)
			{
				projectile.soundDelay = -1;
				Main.PlaySound(2, projectile.Center, 122);
			}
			projectile.ai[0] += 1f;
			if (projectile.ai[0] >= num1125)
			{
				projectile.Kill();
			}
			if (projectile.localAI[0] >= 30f)
			{
				projectile.damage = 0;
				if (projectile.ai[0] < num1125 - 120f)
				{
					float num1126 = projectile.ai[0] % 60f;
					projectile.ai[0] = num1125 - 120f + num1126;
					projectile.netUpdate = true;
				}
			}
			float num1127 = 15f;
			float num1128 = 15f;
			Point point8 = projectile.Center.ToTileCoordinates();
            Collision.ExpandVertically(point8.X, point8.Y, out int num1129, out int num1130, (int)num1127, (int)num1128);
            num1129++;
			num1130--;
			Vector2 value72 = new Vector2(point8.X, num1129) * 16f + new Vector2(8f);
			Vector2 value73 = new Vector2(point8.X, num1130) * 16f + new Vector2(8f);
			Vector2 vector146 = Vector2.Lerp(value72, value73, 0.5f);
			Vector2 value74 = new Vector2(0f, value73.Y - value72.Y);
			value74.X = value74.Y * 0.2f;
			projectile.width = (int)(value74.X * 0.65f);
			projectile.height = (int)value74.Y;
			projectile.Center = vector146;
			if (projectile.owner == Main.myPlayer)
			{
				bool flag74 = false;
				Vector2 center16 = Main.player[projectile.owner].Center;
				Vector2 top = Main.player[projectile.owner].Top;
				for (float num1131 = 0f; num1131 < 1f; num1131 += 0.05f)
				{
					Vector2 position2 = Vector2.Lerp(value72, value73, num1131);
					if (Collision.CanHitLine(position2, 0, 0, center16, 0, 0) || Collision.CanHitLine(position2, 0, 0, top, 0, 0))
					{
						flag74 = true;
						break;
					}
				}
				if (!flag74 && projectile.ai[0] < num1125 - 120f)
				{
					float num1132 = projectile.ai[0] % 60f;
					projectile.ai[0] = num1125 - 120f + num1132;
					projectile.netUpdate = true;
				}
			}
			if (projectile.ai[0] < num1125 - 120f)
			{
				for (int num1133 = 0; num1133 < 1; num1133++)
				{
					float value75 = -0.5f;
					float value76 = 0.9f;
					float amount3 = Main.rand.NextFloat();
					Vector2 value77 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value75, value76, amount3));
					value77.X *= MathHelper.Lerp(2.2f, 0.6f, amount3);
					value77.X *= -1f;
					Vector2 value78 = new Vector2(6f, 10f);
					Vector2 position3 = vector146 + value74 * value77 * 0.5f + value78;
					Dust dust33 = Main.dust[Dust.NewDust(position3, 0, 0, 16, 0f, 0f, 0, default, 1.5f)];
					dust33.position = position3;
					dust33.customData = vector146 + value78;
					dust33.fadeIn = 1f;
					dust33.scale = 0.3f;
					if (value77.X > -1.2f)
					{
						dust33.velocity.X = 1f + Main.rand.NextFloat();
					}
					dust33.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
				}
			}

            for (int u = 0; u < Main.maxNPCs; u++)
            {
                NPC target = Main.npc[u];

                if (target.type != NPCID.TargetDummy && target.active && !target.boss && target.chaseable && target.chaseable && Vector2.Distance(projectile.Center, target.Center) < 150)
                {
                    float num3 = 6f;
                    Vector2 vector = new Vector2(target.position.X + target.width / 2, target.position.Y + target.height / 2);
                    float num4 = projectile.Center.X - vector.X;
                    float num5 = projectile.Center.Y - vector.Y;
                    float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                    num6 = num3 / num6;
                    num4 *= num6;
                    num5 *= num6;
                    int num7 = 6;
                    target.velocity.X = (target.velocity.X * (num7 - 1) + num4) / num7;
                    target.velocity.Y = (target.velocity.Y * (num7 - 1) + num5) / num7;
                    target.velocity *= target.knockBackResist;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
        	float num226 = 600f;
			float num227 = 15f;
			float num228 = 15f;
			float num229 = projectile.ai[0];
			float scale5 = MathHelper.Clamp(num229 / 30f, 0f, 1f);
			if (num229 > num226 - 60f)
			{
				scale5 = MathHelper.Lerp(1f, 0f, (num229 - (num226 - 60f)) / 60f);
			}
            Point point5 = projectile.Center.ToTileCoordinates();
            Collision.ExpandVertically(point5.X, point5.Y, out int num230, out int num231, (int)num227, (int)num228);
            num230++;
			num231--;
			float num232 = 0.2f;
			Vector2 value32 = new Vector2(point5.X, num230) * 16f + new Vector2(8f);
			Vector2 value33 = new Vector2(point5.X, num231) * 16f + new Vector2(8f);
			Vector2.Lerp(value32, value33, 0.5f);
			Vector2 vector33 = new Vector2(0f, value33.Y - value32.Y);
			vector33.X = vector33.Y * num232;
			new Vector2(value32.X - vector33.X / 2f, value32.Y);
			Texture2D texture2D23 = Main.projectileTexture[projectile.type];
            Rectangle rectangle9 = texture2D23.Frame(1, 1, 0, 0);
			Vector2 origin3 = rectangle9.Size() / 2f;
			float num233 = -0.06283186f * num229;
			Vector2 spinningpoint2 = Vector2.UnitY.RotatedBy(num229 * 0.1f, default);
			float num234 = 0f;
			float num235 = 5.1f;
            Color value34 = new Color(225, 225, 225);
			for (float num236 = (int)value33.Y; num236 > (int)value32.Y; num236 -= num235)
			{
				num234 += num235;
				float num237 = num234 / vector33.Y;
				float num238 = num234 * 6.28318548f / -20f;
				float num239 = num237 - 0.15f;
				Vector2 vector34 = spinningpoint2.RotatedBy(num238, default);
				Vector2 value35 = new Vector2(0f, num237 + 1f);
				value35.X = value35.Y * num232;
                Color color39 = Color.Lerp(Color.Transparent, value34, num237 * 2f);
				if (num237 > 0.5f)
				{
					color39 = Color.Lerp(Color.Transparent, value34, 2f - num237 * 2f);
				}
				color39.A = (byte)(color39.A * 0.5f);
				color39 *= scale5;
				vector34 *= value35 * 100f;
				vector34.Y = 0f;
				vector34.X = 0f;
				vector34 += new Vector2(value33.X, num236) - Main.screenPosition;
				Main.spriteBatch.Draw(texture2D23, vector34, new Rectangle?(rectangle9), color39, num233 + num238, origin3, 1f + num239, SpriteEffects.None, 0f);
			}
			return false;
        }
    }

    public class HurricaneSpawn : ModProjectile
    {
        public override string Texture => "AAMod/Projectiles/Athena/Gale";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gale");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 4;
            projectile.timeLeft = 160;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = -1;
            }
            else
            {
                projectile.spriteDirection = 1;
            }
            int num557 = 8;

            int dustId = Dust.NewDust(new Vector2(projectile.position.X + num557, projectile.position.Y + num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 76, 0f, 0f, 0);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X + num557, projectile.position.Y + num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 76, 0f, 0f, 0);
            Main.dust[dustId3].noGravity = true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);

            int h = Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<AthenaHurricane>(), projectile.damage, 0, Main.myPlayer);
            if (projectile.minion)
            {
                Main.projectile[h].magic = false;
                Main.projectile[h].minion = true;
            }
            for (int num579 = 0; num579 < 20; num579++)
            {
                int num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 76, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 2f;
                num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 76, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num580].velocity *= 2f;
            }
        }
    }
}
