using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod;

namespace AAMod.Projectiles.Akuma.Lung
{
    public class Lung : ModProjectile
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Ancient Lung");
		}		

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.netImportant = true;	
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;				
        }

		public bool checkedMinPos = false;
		public static Texture2D[] textures
		{
			get
			{
				return AAMod.instance.texHandler.LungTex;
			}
			set
			{
                AAMod.instance.texHandler.LungTex = value;
			}
		}		
		public static int frameWidth = 40, frameHeight = 40;
		public Rectangle frame = new Rectangle(0, 0, frameWidth, frameHeight);
		
		public int pieceCount = 2, lastPieceCount = 2;
		public Vector2[] piecePositions = new Vector2[2];
		public float[] pieceRots = new float[2];

		public void SetMinionCount(int count)
		{
			pieceCount = count;
			if(lastPieceCount != pieceCount)
			{
				piecePositions = new Vector2[pieceCount];
				pieceRots = new float[pieceCount];	
				projectile.netUpdate2 = true;				
			}
			lastPieceCount = pieceCount;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			int[] projs = BaseAI.GetProjectiles(player.Center, projectile.type, player.whoAmI, -1);
			if(!checkedMinPos)
			{
				for (int m = 0; m < projs.Length; m++) { if (projs[m] == projectile.identity) { projectile.minionPos = m; } }
				if (Main.myPlayer == projectile.owner) { projectile.netUpdate = true; }
			}
			if (!Main.player[projectile.owner].active || Main.player[projectile.owner].dead || projs.Length > 1) { projectile.Kill(); return; }
			SetMinionCount(player.maxMinions);
			int npcTarget = -1;
			BaseAI.AIProjWorm(projectile, ref projectile.ai, ref npcTarget, new int[]{ mod.ProjType("Lung"), -1, -1 }, 1f, 1f, 14f, 9f);
			Vector2 projectileCenter = projectile.Center;
            
            projectile.ai[2]++;
            if (projectile.ai[2] >= 200 && npcTarget != 1)
            {
                projectile.frame = 1;
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X * 3f, projectile.velocity.Y * 3f, mod.ProjectileType("AkumaBreath"), projectile.damage, 0, Main.myPlayer);
            }
            if (projectile.ai[2] >= 260)
            {
                projectile.ai[2] = 0;
                projectile.frame = 0;
            }
			for (int m = 0; m < piecePositions.Length; m++)
			{
				Vector2 lastPos = (m == 0 ? projectile.Center : piecePositions[m - 1]);
				float lastRot = (m == 0 ? projectile.rotation : pieceRots[m - 1]);
				Vector2 pieceCenter = piecePositions[m];
				float pieceRot = pieceRots[m];
				HandleWormPieceMovement(m, lastPos, lastRot, ref pieceCenter, ref pieceRot);
				piecePositions[m] = pieceCenter;
				pieceRots[m] = pieceRot;

				projectile.Center = piecePositions[m];
				projectile.Damage();
			}
			projectile.Center = projectileCenter;
		}
        
		public void HandleWormPieceMovement(int piece, Vector2 otherCenter, float otherRotation, ref Vector2 center, ref float rotation)
		{
			Vector2 centerDist = otherCenter - center;
			if (otherRotation != rotation)
			{
				float rotDist = MathHelper.WrapAngle(otherRotation - rotation);
				centerDist = centerDist.RotatedBy((double)(rotDist * 0.1f), default(Vector2));
			}
			rotation = centerDist.ToRotation() + 1.57079637f;
			if (centerDist != Vector2.Zero)
			{
				center = otherCenter - Vector2.Normalize(centerDist) * 32f;
			}
			if((piece == 0 || piece == 1 || piece == 2) && projectile.velocity.Length() > 6f)
			{
				float length = 6f - projectile.velocity.Length();
				Vector2 vel = projectile.velocity; vel.Normalize();
				vel *= length;
				center -= (piece == 0 ? vel * 1.5f : piece == 1 ? vel * 0.7f : vel * 0.4f);
			}
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			if (textures == null)
			{
				textures = new Texture2D[8];
				textures[0] = Main.projectileTexture[projectile.type];
				textures[1] = mod.GetTexture("Glowmasks/Lung_Glow");		
				textures[2] = mod.GetTexture("Projectiles/Akuma/LungBody");	
				textures[3] = mod.GetTexture("Glowmasks/LungBody_Glow");	
				textures[4] = mod.GetTexture("Projectiles/Akuma/LungBody1");	
				textures[5] = mod.GetTexture("Glowmasks/LungBody2_Glow");					
				textures[6] = mod.GetTexture("Projectiles/Akuma/LungTail");	
				textures[7] = mod.GetTexture("Glowmasks/LungTail_Glow");
			}
			BaseDrawing.DrawTexture(sb, textures[0], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 2, frame, dColor);
			BaseDrawing.DrawTexture(sb, textures[1], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, frame, AAColor.AkumaA);	
			Vector2 projectileCenter = projectile.Center;
			float projectileRot = projectile.rotation;
			for(int m = 0; m < piecePositions.Length; m++)
			{
				if(piecePositions[m] == default(Vector2)) continue;
				projectile.Center = piecePositions[m];
				projectile.rotation = pieceRots[m];
				int texID = (m == piecePositions.Length - 1 ? 6 : (m % 2 == 0 ? 2 : 4));
				//BaseMod.BaseDrawing.DrawHitbox(sb, projectile.Hitbox, Color.Red);
				BaseDrawing.DrawTexture(sb, textures[texID], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, frame, dColor);
				BaseDrawing.DrawTexture(sb, textures[texID + 1], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, frame, AAColor.AkumaA);				
			}
			projectile.Center = projectileCenter;
			projectile.rotation = projectileRot;
			return false;
		}			
    }
}