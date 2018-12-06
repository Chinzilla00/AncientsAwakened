using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace GRealm.Projectiles.Melee
{
	public class TrifectalBall : GProjectile
	{
		public override void SetStaticDefaults()
		{
            displayName = "Demonic Ball";
		}		

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3600;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

		public static Texture2D innerTex;
		public static Texture2D outerTex;
		public static Texture2D chainTex;

		public int master = -1;
		public int ballType = 0; //0 == Demonic (ball o' hurt), 1 == Aqua (blue moon), 2 == Flare (sunfury)
		public static Color[] ballColor = new Color[] { new Color(172, 103, 211), new Color(48, 103, 233), new Color(217, 39, 20) };
		public int masterDelay = 5;
		
		public void CheckMaster()
		{
			if (master >= 0 && (Main.projectile[master] == null || !Main.projectile[master].active || Main.projectile[master].type != mod.ProjType("TrifectalTriball"))) master = -1;
			if (master == -1) 
			{
				master = BaseMod.BaseAI.GetProjectile(projectile.Center, mod.ProjType("TrifectalTriball"), projectile.owner, -1, null);
				if(master == -1) master = -2;
			}			
		}
		
		public override void AI()
		{
			CheckMaster();
			Projectile masterProj = master == -2 ? null : Main.projectile[master];
			Vector2 center = Main.player[projectile.owner].Center;
			Vector2 velocity = Main.player[projectile.owner].velocity;
			if (masterProj == null || !masterProj.active)
			{
				projectile.ai[0] = 1f;
				projectile.tileCollide = false;
			}else
			if (masterProj != null && masterProj.active)
			{
				center = masterProj.Center;
				velocity = masterProj.velocity;
			}
			if (Main.player[projectile.owner] == null || !Main.player[projectile.owner].active || Main.player[projectile.owner].dead) { projectile.Kill(); return; }
			Main.player[projectile.owner].itemAnimation = 10;
			Main.player[projectile.owner].itemTime = 10;
			if (Main.myPlayer == projectile.owner)
			{
				if (master == -2 && Vector2.Distance(projectile.Center, center) <= 15f)
				{
					projectile.Kill();
				}
			}
			if (projectile.active)
			{
				AIFlail(projectile, ref projectile.ai, center, velocity, Main.player[projectile.owner].meleeSpeed, true, 40f);
				if (master != -2 && projectile.ai[0] == 1f)
				{
					if (Math.Abs(masterProj.velocity.X) <= 0.025f) { projectile.velocity.X *= 0.8f; }
					if (Math.Abs(masterProj.velocity.Y) <= 0.025f) { projectile.velocity.Y *= 0.8f; }
				}
			}
		}

		public override void ModifyHitNPC(NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (ballType == 2 && Main.rand.Next(3) == 0) { npc.AddBuff(24, BaseMod.BaseUtility.SecondsToTicks(10)); }
		}

		public override void ModifyHitPvp(Player playerAttacked, ref int damage, ref bool crit)
		{
			if (ballType == 2 && Main.rand.Next(3) == 0 && playerAttacked.whoAmI == projectile.owner)
			{
				playerAttacked.AddBuff(24, BaseMod.BaseUtility.SecondsToTicks(15), false);
			}
		}

		public override bool OnTileCollide(Vector2 value2)
		{
			BaseMod.BaseAI.TileCollideFlail(projectile, ref value2);
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
		{
			if (innerTex == null)
			{
				innerTex = Main.projectileTexture[projectile.type];
				outerTex = GRealm.GetTexture("TrifectalBallSpikes");
				chainTex = GRealm.GetTexture("TrifectalBallChain");
			}
			if (Main.instance.IsActive)
			{
				BaseMod.BaseDrawing.AddLight(projectile.Center, BaseMod.BaseUtility.ColorBrightness(ballColor[ballType], -60));
			}
			Vector2 center = default(Vector2);
			if (master == -2){ center = Main.player[projectile.owner].Center;}else
			if (master != -1){ center = Main.projectile[master].Center; }
			if (center != default(Vector2)){ BaseMod.BaseDrawing.DrawChain(spriteBatch, chainTex, 0, projectile.Center, center); }

			BaseMod.BaseDrawing.DrawTexture(spriteBatch, innerTex, 0, projectile);
			BaseMod.BaseDrawing.DrawTexture(spriteBatch, outerTex, 0, projectile, ballColor[ballType]);
			return false;
		}

		public void AIFlail(Projectile p, ref float[] ai, Vector2 connectedPoint, Vector2 connectedPointVelocity, float meleeSpeed, bool channel, float chainDistance = 160f)
		{
			p.direction = (p.Center.X > connectedPoint.X ? 1 : -1);
			float pointX = connectedPoint.X - p.Center.X;
			float pointY = connectedPoint.Y - p.Center.Y;
			float pointDist = (float)Math.Sqrt((double)(pointX * pointX + pointY * pointY));
			bool rotate = true;
			if (ai[0] == 0f)
			{
				p.tileCollide = true;
				if (pointDist > chainDistance)
				{
					ai[0] = 1f;
					p.netUpdate = true;
				}else
				if (!channel)
				{
					if (p.velocity.Y < 0f) { p.velocity.Y *= 0.9f; }
					p.velocity.Y += 1f;
					p.velocity.X *= 0.9f;
				}
			}else
			if (ai[0] == 1f)
			{
				float meleeSpeed1 = 14f / meleeSpeed;
				float meleeSpeed2 = 0.9f / meleeSpeed;
				float maxBallDistance = chainDistance + 140f;
				Math.Abs(pointX);
				Math.Abs(pointY);
				if (ai[1] == 1f) { p.tileCollide = false; }
				if (!channel || pointDist > maxBallDistance || !p.tileCollide)
				{
					ai[1] = 1f;
					if (p.tileCollide) { p.netUpdate = true; }
					p.tileCollide = false;
					float scale = 1f;
					if (pointDist <= 15f) { rotate = false; scale = 0.5f; }
					if (pointDist <= 5f)
					{
						p.velocity = default(Vector2);
						float pointX1 = Main.player[projectile.owner].Center.X - p.Center.X;
						float pointY1 = Main.player[projectile.owner].Center.Y - p.Center.Y;
						p.rotation = (float)Math.Atan2((double)pointY1, (double)pointX1) - p.velocity.X * 0.1f; return;
					}
					p.velocity *= scale;
				}
				if (!p.tileCollide) { meleeSpeed2 *= 2f; }
				if (pointDist > 60f || !p.tileCollide)
				{
					pointDist = meleeSpeed1 / pointDist;
					pointX *= pointDist;
					pointY *= pointDist;
					new Vector2(p.velocity.X, p.velocity.Y);
					float pointX2 = pointX - p.velocity.X;
					float pointY2 = pointY - p.velocity.Y;
					float pointDist2 = (float)Math.Sqrt((double)(pointX2 * pointX2 + pointY2 * pointY2));
					pointDist2 = meleeSpeed2 / pointDist2;
					pointX2 *= pointDist2;
					pointY2 *= pointDist2;
					p.velocity.X = p.velocity.X * 0.98f;
					p.velocity.Y = p.velocity.Y * 0.98f;
					p.velocity.X = p.velocity.X + pointX2;
					p.velocity.Y = p.velocity.Y + pointY2;
				}else
				{
					if (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y) < 6f)
					{
						p.velocity.X = p.velocity.X * 0.96f;
						p.velocity.Y = p.velocity.Y + 0.2f;
					}
					if (connectedPointVelocity.X == 0f) { p.velocity.X = p.velocity.X * 0.96f; }
				}
				if (master >= 0 && (Math.Abs(Main.projectile[master].velocity.X) <= 0.025f)) { p.velocity.X *= 0.8f; }
				if (master >= 0 && (Math.Abs(Main.projectile[master].velocity.Y) <= 0.025f)) { p.velocity.Y *= 0.8f; }
			}
			if (rotate)
			{
				p.rotation = (float)Math.Atan2((double)pointY, (double)pointX) - p.velocity.X * 0.1f;
			}
		}
	}
}
