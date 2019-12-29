using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofRagnarokProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SpellBook of Ragnarok");
        }
        public override void SetDefaults()
		{
			projectile.damage = 300;
			projectile.width = 108;
			projectile.height = 88;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.timeLeft = 600;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
		}

        public override void AI()
		{
			if (Main.player[projectile.owner].dead || !Main.player[projectile.owner].GetModPlayer<Items.Magic.SpellBook.spellbookplayer>().effectRagnarok || !Main.player[projectile.owner].GetModPlayer<AAPlayer>().SpellBookofRagnarok || Main.player[projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<SpellBookofRagnarokProj>()] > 1)
			{
				projectile.active = false;
				projectile.Kill();
				projectile.netUpdate = true;
				return;
			}

            projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
			projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2) + Main.player[projectile.owner].gfxOffY - 140f;
			if (Main.player[projectile.owner].gravDir == -1f)
			{
				projectile.position.Y = projectile.position.Y + 140f;
				projectile.rotation = 3.14f;
			}
			else
			{
				projectile.rotation = 0f;
			}
			
			projectile.velocity = Vector2.Zero;

			projectile.alpha -= 5;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			if (projectile.timeLeft < 50)
			{
				projectile.alpha += 5;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			if (projectile.direction == 0)
			{
				projectile.direction = Main.player[projectile.owner].direction;
			}
			if (projectile.alpha == 0 && Main.rand.Next(2) == 0)
			{
				Dust dust = Main.dust[Dust.NewDust(projectile.Top, 0, 0, 267, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1f)];
				dust.velocity.X = 0f;
				dust.noGravity = true;
				dust.fadeIn = 1f;
				dust.position = projectile.Center + Utils.RotatedByRandom(Vector2.UnitY, 6.2831854820251465) * (4f * Utils.NextFloat(Main.rand) + 26f);
				dust.scale = 0.5f;
			}
			
			float num = 600f;
			if (projectile.ai[0] < 0f)
			{
				float[] ai = projectile.ai;
				int num4 = 0;
				float num5 = ai[num4];
				ai[num4] = num5 + 1f;
			}
			if (projectile.ai[0] == 0f)
			{
				int num6 = -1;
				float num7 = num;
				if (num6 < 0)
				{
					int num10;
					for (int i = 0; i < 200; i = num10 + 1)
					{
						NPC npc2 = Main.npc[i];
						if (npc2.CanBeChasedBy(projectile, false))
						{
							float num9 = projectile.Distance(npc2.Center);
							if (num9 < num7 && Collision.CanHitLine(projectile.Center, 0, 0, npc2.Center, 0, 0))
							{
								num7 = num9;
								num6 = i;
							}
						}
						num10 = i;
					}
				}
				if (num6 != -1)
				{
					projectile.ai[0] = 1f;
					projectile.ai[1] = (float)num6;
					projectile.netUpdate = true;
					return;
				}
			}
			if (projectile.ai[0] > 0f)
			{
				int num11 = (int)projectile.ai[1];
				if (!Main.npc[num11].CanBeChasedBy(projectile, false))
				{
					projectile.ai[0] = 0f;
					projectile.ai[1] = 0f;
					projectile.netUpdate = true;
					return;
				}
				float[] ai2 = projectile.ai;
				int num12 = 0;
				float num13 = ai2[num12];
				ai2[num12] = num13 + 1f;
				if (projectile.ai[0] >= 5f)
				{
					Vector2 vector = projectile.DirectionTo(Main.npc[num11].Center);
					if (Utils.HasNaNs(vector))
					{
						vector = Vector2.UnitY;
					}
					int direction = (vector.X > 0f) ? 1 : -1;
					projectile.direction = direction;
					projectile.ai[0] = -20f;
					projectile.netUpdate = true;
					if (projectile.owner == Main.myPlayer)
					{
						Vector2 vector2 = Main.npc[num11].position + Main.npc[num11].Size * Utils.RandomVector2(Main.rand, 0f, 1f) - projectile.Center;
						int num14;
						for (int j = 0; j < 5; j = num14 + 1)
						{
							Vector2 vector3 = projectile.Center + vector2;
							if (j > 0)
							{
								vector3 = projectile.Center + Utils.RotatedByRandom(vector2, 0.7853981852531433) * (Utils.NextFloat(Main.rand) * 0.5f + 0.75f);
							}
							float x = Main.rgbToHsl(new Color(Main.DiscoR, 203, 103)).X;
							Projectile.NewProjectile(vector3.X, vector3.Y, 0f, 0f, 644, projectile.damage, projectile.knockBack, projectile.owner, x, (float)projectile.whoAmI);
							num14 = j;
						}
						return;
					}
				}
			}
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Color color = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
			Vector2 vector = projectile.position + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
			Texture2D texture2D = Main.projectileTexture[projectile.type];
			Rectangle rectangle = Utils.Frame(texture2D, 1, Main.projFrames[projectile.type], 0, projectile.frame);
			Color alpha = projectile.GetAlpha(color);
			Vector2 origin = Utils.Size(rectangle) / 2f;
			float scaleFactor = (float)Math.Cos((double)(6.2831855f * (projectile.localAI[0] / 60f))) + 3f + 3f;
			for (float num = 0f; num < 2; num += 1f)
			{
				SpriteBatch spriteBatch2 = Main.spriteBatch;
				Texture2D texture = texture2D;
				Vector2 value = vector;
				Vector2 unitY = Vector2.UnitY;
				spriteBatch2.Draw(texture, value + Utils.RotatedBy(unitY, 0, default(Vector2)) * (num == 0? scaleFactor * 2 : scaleFactor), new Rectangle?(rectangle), num == 0? (alpha * 0.4f) : alpha, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}

    }
}