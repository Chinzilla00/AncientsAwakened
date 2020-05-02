
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Dev
{
    public class CerlaFlowerEX : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cerla Blossom");
		}

		public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 50;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.light = 0.4f;
            projectile.ignoreWater = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
			projectile.alpha = 255;
		}


		public float RingRot = 0;
		public float RingScale = 0;

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();

			if (player.dead)
			{
				modPlayer.CerlaEX = false;
			}
			if (modPlayer.CerlaEX)
			{
				projectile.timeLeft = 2;
			}

			if (projectile.alpha > 0)
			{
				projectile.alpha -= 4;
			}
			else
			{
				projectile.alpha = 0;
			}

			if (RingScale < 1.2f)
			{
				RingScale += .005f;
				RingRot += .05f;
			}
			else
			{
				RingScale = 1.2f;
				RingRot += .02f;
			}

			Vector2 PlayerPoint;
			PlayerPoint.X = player.Center.X - projectile.width / 2;
			PlayerPoint.Y = player.Center.Y - projectile.height / 2 + player.gfxOffY - 60f;

			projectile.position = PlayerPoint;

			if (player.gravDir == -1f)
			{
				projectile.position.Y = projectile.position.Y + 120f;
				projectile.rotation = 3.14f;
			}
			else
			{
				projectile.rotation = 0f;
			}
			if (projectile.owner == Main.myPlayer)
			{
				projectile.ai[0]++;

				float num396 = projectile.position.X;
				float num397 = projectile.position.Y;
				float num398 = 700f;
				bool flag11 = false;
				for (int num399 = 0; num399 < 200; num399++)
				{
					if (Main.npc[num399].CanBeChasedBy(this, true))
					{
						float num400 = Main.npc[num399].position.X + Main.npc[num399].width / 2;
						float num401 = Main.npc[num399].position.Y + Main.npc[num399].height / 2;
						float num402 = Math.Abs(projectile.position.X + projectile.width / 2 - num400) + Math.Abs(projectile.position.Y + projectile.height / 2 - num401);
						if (num402 < num398 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num399].position, Main.npc[num399].width, Main.npc[num399].height))
						{
							num398 = num402;
							num396 = num400;
							num397 = num401;
							flag11 = true;
						}
					}
				}
				if (flag11)
				{
					float num403 = 12f;
					Vector2 vector29 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
					float num404 = num396 - vector29.X;
					float num405 = num397 - vector29.Y;
					float num406 = (float)Math.Sqrt(num404 * num404 + num405 * num405);
					num406 = num403 / num406;
					num404 *= num406;
					num405 *= num406;
					if (projectile.ai[0] % 10 == 0)
					{
						Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, num404, num405, ModContent.ProjectileType<CerlaProj>(), (int)(projectile.damage * player.minionDamage), Player.crystalLeafKB, projectile.owner, 1, 0f);
					}

				}
			}
		}

        public float auraPercent = 0f;
        public bool auraDirection = true;

		public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
			Texture2D ring = mod.GetTexture("Projectiles/Dev/CerlaRuneEX");
			Player player = Main.player[projectile.owner];

			if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

			Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);
			Rectangle ringFrame = BaseDrawing.GetFrame(projectile.frame, ring.Width, ring.Height, 0, 0);

			BaseDrawing.DrawTexture(sb, ring, 0, projectile.position, projectile.width, projectile.height, RingScale, RingRot, projectile.direction, 1, ringFrame, projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);

			BaseDrawing.DrawAura(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, auraPercent, 1.4f, projectile.scale, projectile.rotation, -player.direction, 1, frame, 0, 0, Color.White);
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, -player.direction, 1, frame, projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);


			return false;
        }
	}
}
