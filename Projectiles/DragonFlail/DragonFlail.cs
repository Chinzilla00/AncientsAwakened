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
	public class TrifectalTriball : GProjectile
	{
		public override void SetStaticDefaults()
		{
            displayName = "Trifectal Tri-Ball";
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
		public static Texture2D connectTex;

		public int[] balls = new int[3];
		public static Color[] ballColor = new Color[] { new Color(172, 103, 211), new Color(48, 103, 233), new Color(217, 39, 20) };
		public bool firedBalls = false;
		
		public override void AI()
		{
			BaseMod.BaseAI.AIFlail(projectile, ref projectile.ai, false, 200f);
			if (Main.myPlayer == projectile.owner)
			{
				if (projectile.active && projectile.ai[0] == 1)
				{
					if (!firedBalls)
					{
						firedBalls = true;
						float fireSpeed = 10f;
						for (int m = 0; m < balls.Length; m++)
						{
							Vector2 offsetVec = (m == 0 ? new Vector2(12, 0) : m == 1 ? new Vector2(-6, 12) : new Vector2(-6, -12));
							Vector2 rotVec = BaseMod.BaseUtility.RotateVector(projectile.Center, projectile.Center + offsetVec, BaseMod.BaseUtility.RotationTo(Main.player[projectile.owner].Center, projectile.Center));
							float speedX = (float)rotVec.X - projectile.Center.X;
							float speedY = (float)rotVec.Y - projectile.Center.Y;
							float dist = (float)System.Math.Sqrt((double)(speedX * speedX + speedY * speedY));
							dist = fireSpeed / dist;
							speedX *= dist;
							speedY *= dist;
							balls[m] = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speedX, speedY, mod.ProjType("TrifectalBall" + (m == 0 ? "" : "" + (m + 1))), projectile.damage, projectile.knockBack, projectile.owner);
						}
					}
				}
			}
		}

		public override bool OnTileCollide(Vector2 value2)
		{
			BaseMod.BaseAI.TileCollideFlail(projectile, ref value2);
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			if (innerTex == null)
			{
				innerTex = Main.projectileTexture[projectile.type];
				outerTex = GRealm.GetTexture("TrifectalBallSpikes");
				chainTex = GRealm.GetTexture("TrifectalBallChain");
				connectTex = GRealm.GetTexture("TrifectalBallConnect");
			}
			BaseMod.BaseDrawing.DrawChain(spriteBatch, chainTex, 0, projectile.Center, Main.player[projectile.owner].Center);
			Color lightColor = BaseMod.BaseDrawing.GetLightColor(projectile.Center);
			if (projectile.ai[0] == 0)
			{
				for (int m = 0; m < balls.Length; m++)
				{
					Vector2 offsetVec = (m == 0 ? new Vector2(12, 0) : m == 1 ? new Vector2(-6, 12) : new Vector2(-6, -12));
					Vector2 rotVec = BaseMod.BaseUtility.RotateVector(projectile.position, projectile.position + offsetVec, projectile.rotation);
					Vector2 rotVecCenter = rotVec + new Vector2(projectile.width * 0.5f, projectile.height * 0.5f);
					if (Main.instance.IsActive)
					{
						BaseMod.BaseDrawing.AddLight(rotVecCenter, BaseMod.BaseUtility.ColorBrightness(ballColor[m], -60));
					}
					BaseMod.BaseDrawing.DrawTexture(spriteBatch, innerTex, 0, rotVec, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, new Rectangle(0, 0, innerTex.Width, innerTex.Height), lightColor);
					BaseMod.BaseDrawing.DrawTexture(spriteBatch, outerTex, 0, rotVec, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, new Rectangle(0, 0, outerTex.Width, outerTex.Height), ballColor[m]);
				}
			}else
			{
				BaseMod.BaseDrawing.DrawTexture(spriteBatch, connectTex, 0, projectile);
			}
			return false;
		}
	}
}